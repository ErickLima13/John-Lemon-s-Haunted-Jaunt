using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public Transform[] waypoints;

    private int m_CurrentWaypointIndex;
    
    private void Initialization()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Start()
    {
        Initialization();
    }

    
    void Update()
    {
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
