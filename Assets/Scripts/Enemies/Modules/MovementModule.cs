using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyBase))]
public class MovementModule : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    private Transform player;
    private EnemyData enemyData;

    private bool playerDetected = false;
    private bool lostPlayer = true;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        enemyData = GetComponent<EnemyBase>().enemyData;
    }


    void Update()
    {
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);
        float distanceZ = Mathf.Abs(player.position.z - transform.position.z);
        float distance = Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2);

        if (distance < Mathf.Pow(enemyData.visionRange, 2))
        {
            playerDetected = true;
            lostPlayer = false;
        }
        else if (distance > Mathf.Pow(enemyData.visionRange + enemyData.attackRange, 2))
        {
            playerDetected = false;

            if (!lostPlayer)
            {
                lostPlayer = true;
            }
        }

        if (!playerDetected) return; // Player detected area

        if (distance > Mathf.Pow(enemyData.attackRange, 2))
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }
}
