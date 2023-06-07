using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movements : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Vector3 _target;

    public event Action Moved;
    
    public NavMeshAgent Agent => _agent;


    private void Awake() => _agent = GetComponent<NavMeshAgent>();

    public void SetSpeed(float moveSpeed) => _agent.speed = moveSpeed;

    public void SetTarget(Vector3 target)
    {
        _agent.SetDestination(target);
        _target = target;
        Moved?.Invoke();
    }
}
