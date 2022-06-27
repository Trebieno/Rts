using UnityEngine;
using UnityEngine.AI;

public class Enemy : Weapon, IHealth
{
    private enum Type
    {
        Warrior,
        Archer,
    };
    [SerializeField] private Type typeEnemy = Type.Warrior;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _curHealth = 100;
    [SerializeField] private float _moveSpeed;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _moveSpeed;
        _isCloseCombat = true;
        _isRadius = false;
        if (typeEnemy == Type.Archer)
        {
            _isCloseCombat = false;
            _isRadius = true;
        }
    }

    private void FixedUpdate() 
    {
        if(SearchEnemy(gameObject).Count > 0 && Vector3.Distance(SearchEnemy(gameObject)[0].transform.position, transform.position) > _radius)
            _agent.SetDestination(SearchEnemy(gameObject)[0].transform.position);
        

        else if (SearchEnemy(gameObject).Count > 0 && Vector3.Distance(SearchEnemy(gameObject)[0].transform.position, transform.position) < _radius)
            _target = SearchEnemy(gameObject)[0].transform;

        
        if(_target != null)
            _agent.SetDestination(transform.position);
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

    protected override void RotatinonObjectOrMove()
    {
        
    }

    public void LvlUp()
    {
        _maxHealth += (_maxHealth * 10) / 100;
        _damage += (_damage * 10) / 100;
        _moveSpeed += (_damage * 5) / 100;
        _curHealth = _maxHealth;
    }
}
