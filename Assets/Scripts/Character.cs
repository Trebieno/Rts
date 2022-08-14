using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public enum Commands
{
    unit, enemy, tower
}
[RequireComponent(typeof(Movements))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(MeleeAttack))]
[RequireComponent(typeof(RangeAttack))]
[RequireComponent(typeof(AnimationHandler))]
public class Character : MonoBehaviour, IAttackeble
{
    public Movements Movements;
    public NpcView NpcView;
    public Commands Command;
    public event Action Damaged;
    


    protected AnimationHandler animationHandler;
    
    protected Attacker attacker;
    
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float curHealth;
    [SerializeField] protected float moveSpeed;

    [SerializeField] protected bool isControlling;
    private IAttack _attack;
    private void Start()
    {
        Movements = GetComponent<Movements>();
        animationHandler = GetComponent<AnimationHandler>();
        attacker = GetComponent<Attacker>();
        NpcView = GameObject.Find("NpcView").GetComponent<NpcView>();
        NpcView.AddList(this);
    }

    public void SetDamage(float damage)
    {
        Damaged?.Invoke();
        curHealth -= damage;
        if (curHealth <= 0) Destroy(gameObject);

        Debug.Log(gameObject.name +" : "+ curHealth);
    }    
}
