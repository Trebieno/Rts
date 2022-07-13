using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dest : MonoBehaviour
{
    [SerializeField] private Vector3 dest;

    [ContextMenu("go")]
    private void NewDest()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(dest);
    }

}
