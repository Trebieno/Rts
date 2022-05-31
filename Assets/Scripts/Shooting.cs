using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _targetEnemy;
    [SerializeField] private float _bulletForce = 35;
    [SerializeField] private float _delya = 0.3f;
    [SerializeField] private float _reload = 1.5f;
    [SerializeField] private float _damage = 30;
    [SerializeField] private float _radius;
    [SerializeField] private List<GameObject> _otherObjects;
    [SerializeField] private List<GameObject> _nearestObjects;
    [SerializeField] private int _curBullets = 30; 
    [SerializeField] private int _maxBullets = 30; 

    private bool _isFire;
    private bool _isReload;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private void Update()
    {
        if(ConnectingToDataBase(gameObject).Count > 0)
        {
            _targetEnemy = ConnectingToDataBase(gameObject)[0].transform;
            transform.LookAt(new Vector3(_targetEnemy.position.x, 0.5f, _targetEnemy.position.z));
        }            

        if (_targetEnemy != null)
            _isFire = true;

        if (_targetEnemy == null)
            _isFire = false;

        if (_isFire && i >= 1)
        {
            Shoot();
            i = 0;
        }
    }
    
    private void Shoot()
    {
        Debug.Log("Выстрел");
        RaycastHit hit;
        Debug.DrawRay(_firePoint.position, _firePoint.up * _radius, Color.red, 0.2f);
        if (Physics.Raycast(_firePoint.position, _firePoint.up, out hit, _radius, _enemyMask) && _curBullets > 0)
        {
            Debug.Log("Попал в " + hit.transform.name);
            if (hit.transform.GetComponent<IHealth>() != null)
            {
                IHealth health = hit.transform.GetComponent<IHealth>();
                health.TakeDamage(_damage);
                _curBullets--;
            }
        }
        else if (_curBullets <= 0 && !_isReload)
        {
            StartCoroutine(Reload());
        }            
    }

    private int i = 0;
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delya);
        i++;
        StartCoroutine(Delay());
    }

    private IEnumerator Reload()
    {
        _isReload = true;
        yield return new WaitForSeconds(_reload);
        _curBullets = _maxBullets;
        _isReload = false;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;        
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private List<GameObject> ConnectingToDataBase(GameObject Object)
    {
        _otherObjects = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        _nearestObjects = _otherObjects.Where(x => Vector3.Distance(Object.transform.position, x.transform.position) < _radius).ToList();
        _nearestObjects = _nearestObjects.OrderBy(x => Vector3.Distance(Object.transform.position, x.transform.position)).ToList();

        return _nearestObjects;
    }
}