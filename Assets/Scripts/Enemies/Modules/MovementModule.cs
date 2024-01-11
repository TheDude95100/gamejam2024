using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyBase))]
public class MovementModule : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private EnemyData enemyData;
    private EnemyBase enemyBase;

    private bool playerDetected = false;
    private bool lostPlayer = true;
    public bool groupHasDetectedPlayer = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        enemyBase = GetComponent<EnemyBase>();
        enemyData = enemyBase.enemyData;
    }


    void Update()
    {
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);
        float distanceZ = Mathf.Abs(player.position.z - transform.position.z);
        float distance = Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2);

        if (distance < Mathf.Pow(enemyData.visionRange, 2))
        {
            playerDetected = true;
            EnemiesManager.Instance.NoticePlayerDetected(enemyBase.groupID);

            lostPlayer = false;
        }
        else if (distance > Mathf.Pow(enemyData.visionRange + enemyData.attackRange, 2))
        {
            playerDetected = false;

            if (!lostPlayer)
            {
                lostPlayer = true;
                EnemiesManager.Instance.NoticePlayerLost(enemyBase.groupID);
            }
        }

        if (!playerDetected && !groupHasDetectedPlayer) return; // Player detected area

        // Stop moving if player is in attack range
        if (distance > Mathf.Pow(enemyData.attackRange, 2))
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    public bool HasLostPlayer()
    {
        return lostPlayer;
    }
}
