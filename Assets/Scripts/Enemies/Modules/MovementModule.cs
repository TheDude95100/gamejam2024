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

        if (!groupHasDetectedPlayer) return; // Player detected area

        // Stop moving if player is in attack range
        if (distance > Mathf.Pow(enemyData.attackRange, 2))
        {
            Debug.Log("Agent is in range, moving to player");
            agent.SetDestination(player.position);
        }
        else
        {
            Debug.Log("Agent is in attack range, stopping");
            agent.SetDestination(transform.position);
        }
    }

    public bool HasLostPlayer()
    {
        return lostPlayer;
    }

    internal void GroupHasLostPlayer()
    {
        // Random point in a circle around the last known position
        Vector3 randomDirection = Random.insideUnitSphere * enemyData.visionRange;
        randomDirection += player.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, enemyData.visionRange, 1);
        Vector3 finalPosition = hit.position;

        // DEBUG : Draw a cross on the random position
        Debug.DrawLine(finalPosition + Vector3.up * 5, finalPosition - Vector3.up * 5, Color.red, 10);
        Debug.DrawLine(finalPosition + Vector3.right * 5, finalPosition - Vector3.right * 5, Color.red, 10);
        Debug.DrawLine(finalPosition + Vector3.forward * 5, finalPosition - Vector3.forward * 5, Color.red, 10);

        Debug.Log("Agent has lost the player, moving to " + finalPosition);
        agent.SetDestination(finalPosition);
    }
}
