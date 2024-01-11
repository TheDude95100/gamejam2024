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
    private Transform roamingPosition; // Roaming position is the position of the agent when it is not chasing the player

    private EnemyData enemyData;
    private EnemyBase enemyBase;

    private bool playerDetected = false;
    private bool lostPlayer = true;
    private bool groupHasDetectedPlayer = false;

    private bool invoked = false;

    private enum State
    {
        Roaming,
        Chasing,
        Attacking
    }
    private State state;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyBase = GetComponent<EnemyBase>();
        enemyData = enemyBase.enemyData;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyData.speed;
        agent.angularSpeed = enemyData.angularSpeed;
        agent.acceleration = enemyData.acceleration;
        agent.stoppingDistance = enemyData.stoppingDistance;
    }


    void Start()
    {
        roamingPosition = transform;
        state = State.Roaming;
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
            Debug.Log("Agent is in range, moving to player : " + groupHasDetectedPlayer);
            agent.SetDestination(player.position);

            state = State.Chasing;
        }
        else
        {
            Debug.Log("Agent is in attack range, stopping");
            agent.SetDestination(transform.position);

            // Face the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * enemyData.angularSpeed * 0.1f);

            state = State.Attacking;
        }
    }


    public bool HasLostPlayer()
    {
        return lostPlayer;
    }

    internal void GroupHasLostPlayer()
    {
        Debug.Log("Group has lost the player");
        groupHasDetectedPlayer = false;

        // Random point in a circle around the last known position
        Vector3 lastSeen = RandomNavSphere(player.position, enemyData.visionRange);

        // DEBUG : Draw a cross on the random position
        Debug.DrawLine(lastSeen + Vector3.up * 5, lastSeen - Vector3.up * 5, Color.red, 2);
        Debug.DrawLine(lastSeen + Vector3.right * 5, lastSeen - Vector3.right * 5, Color.red, 2);
        Debug.DrawLine(lastSeen + Vector3.forward * 5, lastSeen - Vector3.forward * 5, Color.red, 2);

        Debug.Log("Agent has lost the player, moving to " + lastSeen);
        agent.SetDestination(lastSeen);

        state = State.Chasing;
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, distance, 1);
        return hit.position;
    }

    internal void PlayerDetected()
    {
        float randomTime = Random.Range(0.1f, 1f);
        if (invoked) return;
        Invoke("SetGroupDetected", randomTime);
        invoked = true;
    }

    internal void SetGroupDetected()
    {
        groupHasDetectedPlayer = true;
        invoked = false;
    }

    private void OnDrawGizmos()
    {
        if (state == State.Roaming)
        {
            Gizmos.color = Color.green;
        }
        else if (state == State.Chasing)
        {
            Gizmos.color = Color.yellow;
        }
        else if (state == State.Attacking)
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
    }
}
