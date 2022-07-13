using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public enum Commands
{
    unit, enemy
}

[RequireComponent(typeof(Movements))]
[RequireComponent(typeof(MeleeAttack))]
[RequireComponent(typeof(RangeAttack))]
public class Character : MonoBehaviour, IAttackeble
{
    [HideInInspector] public Movements Movements;
    public Commands Command;
    [SerializeField] private LayerMask _attackMask;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float curHealth;
    [SerializeField] protected float moveSpeed;

    
    [SerializeField] private float _radius;
    [SerializeField] private bool _isRadius;
    [SerializeField] private bool _isMelleeAttack;
    [SerializeField] private bool _isRangeAttack;
    [SerializeField] private bool _isControlling;

    private NpcView _npcView;
    private IAttack _attack;
    private MeleeAttack _meleeAttack;
    private RangeAttack _rangeAttack;
    
    protected NpcView npcView;

    private void Awake()
    {
        npcView = GameObject.Find("NpcView").GetComponent<NpcView>();
        npcView.AddList(this);
        _meleeAttack = GetComponent<MeleeAttack>();
        _rangeAttack = GetComponent<RangeAttack>();
    }

    private void FixedUpdate() 
    {
        //Movements?.SetTarget(Searching(this)[0].position);
    }

    private void Attack()
    {
        
    }

    public List<Transform> Searching(Character character)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius, _attackMask);
        List<Transform> _nearestObjects = new List<Transform>(hitColliders.Length);
        _nearestObjects = _nearestObjects.Where(x => Vector3.Distance(character.transform.position, x.transform.position) < 8f).ToList();
        _nearestObjects = _nearestObjects?.OrderBy(x => Vector3.Distance(character.transform.position, x.transform.position)).ToList();
        return _nearestObjects;
    }

    public void SetDamage(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
            Destroy(gameObject);

        Debug.Log(curHealth +" "+ gameObject.name);
    }

    public void OnDrawGizmos() 
    {
        if(!_isRadius) return;
        Gizmos.color = Color.red;        
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
