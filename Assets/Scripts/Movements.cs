using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movements : MonoBehaviour
{
    private NavMeshAgent _agent;
    public event Action Moved;
    private Vector3 _target;

    private void Awake() => _agent = GetComponent<NavMeshAgent>();

    public void SetSpeed(float moveSpeed) => _agent.speed = moveSpeed;

    public void SetTarget(Vector3 target)
    {
        _agent.SetDestination(target);
        _target = target;
        Moved?.Invoke();
    }

    public bool GetStop() => ((Vector3.Distance(transform.position, _target) > 0.4f) ? false : true);
}
