using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#if TANK
public class EnemyController : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}
#endif