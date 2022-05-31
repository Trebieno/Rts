using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _targetEnemy;
    [SerializeField] private float _bulletForce = 35;
    [SerializeField] private float _delya = 0.3f;
    [SerializeField] private float _damage = 30;
    [SerializeField] private float _radius;
    [SerializeField] private float _reload = 1.5f;
    [SerializeField] private int _curBullets = 30; 
    [SerializeField] private int _maxBullets = 30; 


    private bool _isFire;
    private bool _isReload;

    private Enemy enemy;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        StartCoroutine(Delay());
    }

    void Update()
    {
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
        Debug.DrawRay(_firePoint.position, _firePoint.forward * _radius, Color.red, 0.2f);
        if (Physics.Raycast(_firePoint.position, _firePoint.forward, out hit, _radius, _enemyMask) && _curBullets > 0)
        {
            Debug.Log("Попал в " + hit.transform.name);
            if (hit.transform.GetComponent<IHealth>() != null)
            {
                IHealth health = hit.transform.GetComponent<IHealth>();
                health.TakeDamage(_damage);
                _curBullets--;
            }
        }
        else
            Reload();
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
        yield return new WaitForSeconds(_reload);
        _curBullets = _maxBullets;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;        
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void SetTarget(Transform target)
    {
        if(Vector3.Distance(target.position, transform.position) <= _radius)
        {
            _targetEnemy = target;
            enemy.Move(transform.position);
        }
        else
            enemy.Move(target.position);
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
}
