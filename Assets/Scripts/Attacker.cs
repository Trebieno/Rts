using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attacker : MonoBehaviour, IAttack
{
    private Character _character;
    private IAttack _attack;

    private MeleeAttack _meleeAttack;
    private RangeAttack _rangeAttack;
    [SerializeField] private LayerMask _attackMask;
    [SerializeField] private float _radius; 
    [SerializeField] private bool _isMelleeAttack;
    [SerializeField] private bool _isRangeAttack;
    [SerializeField] private bool _isRadius;    

    private float damage = 5; 

    private void Start()
    {
        _character = GetComponent<Character>();
        _attack = GetComponent<Attacker>();
        _meleeAttack = GetComponent<MeleeAttack>();
        _rangeAttack = GetComponent<RangeAttack>();
    }

    private void FixedUpdate()
    {
        // Character enemy = _character.NpcView.SearchingNearest(_character, _radius);
        // if (enemy != null)
        //     _attack.DealDamage(enemy);
        
    }

    public void Test()
    {
        try
        {
            // _attack.DealDamage(_character.NpcView.SearchingNearest(_character, _radius));
        }
        catch (System.Exception)
        {
            Debug.Log("Нет в радиусе");
        }
        
    }

    public void Attack()
    {
        
    }

    public void DealDamage(IAttackeble attackeble) => attackeble.SetDamage(damage);

    public void OnDrawGizmos() 
    {
        if(!_isRadius) return;
        Gizmos.color = Color.red;        
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
