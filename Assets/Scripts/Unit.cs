using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, IHealth
{
    [SerializeField] private GameObject _selectedSprite;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private bool _isSelected = false;
    private bool previousIsSelected = false;
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private List<Unit> _squad;
    [SerializeField] private float _curHealth = 20;
    [SerializeField] private float _maxHealth = 20;
    [SerializeField] private float _speed = 5;

    private Vector3 _target = Vector3.zero;
    private Unit unit;
    private void Awake()
    {
        unit = GetComponent<Unit>();
        _squad.Add(unit); 
        _agent = GetComponent<NavMeshAgent>();        
        _selectedSprite.SetActive(false);
        UnitSelection.units.Add(this);
    }

    private void Update()
    {                               
        if (_isMoving)
        {
            _agent.SetDestination(_target);
        }
    }

    public void MoveTo(Vector3 point)
    {
        SetMoving(true);
        _target = point;
    }

    public void SquadSelect(bool isSelected)
    {
        if (isSelected != previousIsSelected) 
        {     
            _squad.RemoveAll(x => x == null);
            for (int i = 0; i < _squad.Count; i++)
            {
                _squad[i].SetSelect(isSelected);
                _squad[i].SetSprite(isSelected);
            }
        }
        previousIsSelected = isSelected;
    }
    
    public void Attack()
    {

    }

    public List<Unit> AllSquadMembers()
    {
        return _squad;
    }

    public void SetSelect(bool isSelected)
    {
        this._isSelected = isSelected;

    }

    public void SetMoving(bool isMoving)
    {
        this._isMoving = isMoving;
    }

    public void SetSprite(bool active)
    {
        _selectedSprite.SetActive(active);
    }

    public bool IsSelected()
    {
        return _isSelected;
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
}
