using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movements : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake() => _agent = GetComponent<NavMeshAgent>();

    public void SetSpeed(float moveSpeed) => _agent.speed = moveSpeed;
    
    public void SetTarget(Vector3 target) => _agent.SetDestination(target);
}
