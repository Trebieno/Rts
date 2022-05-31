using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHealth
{
    private enum Type
    {
        warrior,
        Archer,
    };
    [SerializeField] private Type typeEnemy = Type.warrior;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _curHealth = 100;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _damagePoint;
    [SerializeField] private LayerMask _unitsMask;
    private NavMeshAgent _agent;
    private EnemyShooting enemyShooting;

    private void Awake()
    {
        enemyShooting = GetComponent<EnemyShooting>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _moveSpeed;
        StartCoroutine(Hiting());
    }

    
    private void Update()
    {
        if(_target != null)
        {
            Move(_target.position);

            switch(typeEnemy)
            {
            case Type.Archer:
                ArcherAttack();
                break;
            case Type.warrior:
                WarriorAttack();
                break;
            }

        }
        
        if(_target == null)
            SearhUnit();

    }

    private int hit = 0;
    private IEnumerator Hiting()
    {
        yield return new WaitForSeconds(0.3f);
        hit++;

        StartCoroutine(Hiting());
    }

    public void TakeDamage(float damage)
    {
        _curHealth -= damage;
        if (_curHealth <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log(_curHealth +"  "+ gameObject.name);
    }

    private void SearhUnit()
    {
        List<GameObject> _units = GameObject.FindGameObjectsWithTag("Unit").ToList();
        if(_target == null && _units.Count > 0)
        {
            _target = _units[Random.Range(0, _units.Count - 1)].transform;
        }
    }

    private void WarriorAttack()
    {
        Debug.DrawRay(_damagePoint.position, _damagePoint.forward, Color.red, 0.02f);
        RaycastHit hit;
        if (Physics.Raycast(_damagePoint.position, _damagePoint.forward, out hit, 0.02f,_unitsMask))
        {            
            if (hit.transform.GetComponent<IHealth>() != null)
            {
                IHealth health = hit.transform.GetComponent<IHealth>();
                if (this.hit >= 1)
                {
                    health.TakeDamage(_damage);
                    this.hit = 0;
                }
            }
        }
    }

    private void ArcherAttack()
    {
        enemyShooting.SetDamage(_damage);
        enemyShooting.SetTarget(_target);        
    }

    public void Move(Vector3 target)
    {
        _agent.SetDestination(_target.position);
    }
}
