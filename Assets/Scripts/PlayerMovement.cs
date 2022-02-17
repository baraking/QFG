using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //navMeshAgent.baseOffset = Constants.CHARACTER_HEIGHT / 2;
    }

    public void WalkToPoint(Vector3 point)
    {
        navMeshAgent.SetDestination(point);
    }
}
