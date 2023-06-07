using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public enum Team
{
    unit, enemy
}

public abstract class Character : MonoBehaviour, IAttackeble
{
    public event Action Damaged;

    [SerializeField] protected Movements movements;
    [SerializeField] protected Team team;
    [SerializeField] protected AnimationHandler animationHandler;
    [SerializeField] protected Attacker attacker;    
    protected float maxHealth;

    [SerializeField] protected float curHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected bool isControlling;


    public Movements Movements => movements;
    public Team Team => team;
    public AnimationHandler AnimationHandler => animationHandler;
    public Attacker Attacker => attacker;

    private void Awake()
    {
        maxHealth = curHealth;        
    }

    private void Start()
    {        
        if(movements == null)
        {
            movements = GetComponent<Movements>();
            movements.Agent.speed = moveSpeed;
        }

        if(animationHandler == null)
            animationHandler = GetComponent<AnimationHandler>();
        
        if(attacker == null)
            attacker = GetComponent<Attacker>();
    }

    public void SetDamage(float damage)
    {
        Damaged?.Invoke();
        curHealth -= damage;

        if (curHealth <= 0) Die();
    }    

    private void Die()
    {
        Destroy(this);
    }
}
