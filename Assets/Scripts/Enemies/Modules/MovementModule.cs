using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

[RequireComponent(typeof(EnemyBase))]
public class MovementModule : MonoBehaviour
{
    private NavMeshAgent agent;
    private NavMeshObstacle selfObstacle;

    private Transform player;
    private Vector3 target;
    private EnemyData enemyData;
    private EnemyBase enemyBase;
    private AbilityData abilityData;

    private bool playerDetected = false;
    private bool lostPlayer = true;
    private bool groupHasDetectedPlayer = false;

    private bool invoked = false;

    public bool locked = false;

    private enum State
    {
        Idle,
        Moving,
        Attacking
    }
    private State state = State.Idle;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyBase = GetComponent<EnemyBase>();
        enemyData = enemyBase.enemyData;

        try { 
            abilityData = enemyBase.abilityData[0]; 
        }
        catch {
            Debug.LogError("Enemy " + gameObject.name + " has no abilityData[0]");
            // Create the default abilityData
            abilityData = new AbilityData();
        }

        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyData.speed;
        agent.angularSpeed = enemyData.angularSpeed;
        agent.acceleration = enemyData.acceleration;
        agent.stoppingDistance = enemyData.stoppingDistance;

        selfObstacle = GetComponent<NavMeshObstacle>();
        if (selfObstacle == null && enemyData.damage > 1 && enemyData.targetSurrounding)
        {
            Debug.LogError("Enemy " + gameObject.name + " has no NavMeshObstacle component despite having damage > 0 and targetSurrounding = true");
        }
    }


    void Start()
    {
        // Random obstacle avoidance
        //agent.avoidancePriority = Random.Range(0, 100); // This is worst

        // Random radius avoidance
        agent.radius = Random.Range(enemyData.minRandomRadiusAvoidanceRange, enemyData.maxRandomRadiusAvoidanceRange);
    }


    void Update()
    {
        if (locked) return;

        float distanceX = Mathf.Abs(player.position.x - transform.position.x);
        float distanceZ = Mathf.Abs(player.position.z - transform.position.z);
        float distance = Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2);

        if (distance < Mathf.Pow(enemyData.visionRange, 2))
        {
            playerDetected = true;
            EnemiesManager.Instance.NoticePlayerDetected(enemyBase.groupID);

            lostPlayer = false;
        }
        else if (distance > Mathf.Pow(enemyData.visionRange + abilityData.AttackRange, 2))
        {
            playerDetected = false;

            if (!lostPlayer)
            {
                lostPlayer = true;
                EnemiesManager.Instance.NoticePlayerLost(enemyBase.groupID);
            }
        }

        if (!groupHasDetectedPlayer) return; // Player detected area

        if (enemyData.damage > 0)
        {
            HostileMovement();
        }
        else
        {
            PassiveMovement();
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
        CancelInvoke();
        invoked = false;

        // Random point in a circle around the last known position
        Vector3 lastSeen = RandomNavSphere(player.position, enemyData.visionRange);

        // DEBUG : Draw a cross on the random position
        Debug.DrawLine(lastSeen + Vector3.up * 5, lastSeen - Vector3.up * 5, Color.red, 2);
        Debug.DrawLine(lastSeen + Vector3.right * 5, lastSeen - Vector3.right * 5, Color.red, 2);
        Debug.DrawLine(lastSeen + Vector3.forward * 5, lastSeen - Vector3.forward * 5, Color.red, 2);

        Debug.Log("Agent has lost the player, moving to " + lastSeen);

        if (selfObstacle != null) selfObstacle.enabled = false;

        agent.enabled = true;

        if (enemyData.damage > 0) agent.SetDestination(lastSeen);

        state = State.Moving;
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
        Debug.Log("group has detected the player");
        groupHasDetectedPlayer = true;
        invoked = false;
    }

    private void HostileMovement()
    {
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);
        float distanceZ = Mathf.Abs(player.position.z - transform.position.z);
        float distance = Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2);

        // Stop moving if player is in attack range
        if (distance > Mathf.Pow(abilityData.AttackRange, 2))
        {
            Debug.Log("Agent is in range, moving to player : " + groupHasDetectedPlayer);
            target = player.position;

            state = State.Moving;
        }
        else
        {
            Debug.Log("Agent is in attack range, stopping");
            target = transform.position;

            // Face the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * enemyData.angularSpeed * 0.1f);

            state = State.Attacking;
        }

        if (!enemyData.targetSurrounding)
        {
            agent.SetDestination(target);
            return;
        }

        // If target is not reached, update target
        float distanceX2 = Mathf.Abs(target.x - transform.position.x);
        float distanceZ2 = Mathf.Abs(target.z - transform.position.z);
        float distance2 = Mathf.Pow(distanceX2, 2) + Mathf.Pow(distanceZ2, 2);

        if (distance2 > Mathf.Pow(enemyData.stoppingDistance, 2))
        {
            selfObstacle.enabled = false;

            // find nearest point on navmesh to move to
            Vector3 testTarget = target;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 1.1f, NavMesh.AllAreas))
                testTarget = hit.position;

            // DEBUG : Draw a cross on the random position
            Debug.DrawLine(testTarget + Vector3.up * 2, testTarget - Vector3.up * 2, Color.green);
            Debug.DrawLine(testTarget + Vector3.right * 2, testTarget - Vector3.right * 2, Color.green);
            Debug.DrawLine(testTarget + Vector3.forward * 2, testTarget - Vector3.forward * 2, Color.green);

            // if the point is further away (blocked), do noting
            if (Vector3.Distance(transform.position, testTarget) > 1.1f)
                return;

            if (agent.isOnNavMesh) agent.SetDestination(target);

            agent.enabled = true;

        }
        else
        {
            selfObstacle.enabled = true;
            agent.enabled = false;
        }
    }

    private void PassiveMovement()
    {
        state = State.Moving;

        // Calculate the direction away from the player
        Vector3 fleeDirection = transform.position - player.transform.position;
        fleeDirection.Normalize(); // Normalize the vector to get only the direction

        // Set the distance multiplier (1.5 times the vision range)
        float fleeDistanceMultiplier = 1.5f;

        // Calculate the distance based on the vision range
        float fleeDistance = enemyData.visionRange * fleeDistanceMultiplier;

        // Set the frequency of the zigzag movement
        float zigzagFrequency = 2f; // Adjust this value to control the frequency

        // Calculate the offset using sine function to create zigzag movement
        float zigzagOffset = Mathf.Sin(Time.time * zigzagFrequency);

        // Apply the offset to the direction to create zigzag movement
        Vector3 zigzagDirection = fleeDirection + new Vector3(zigzagOffset, 0f, 0f);

        // Normalize the zigzag direction
        zigzagDirection.Normalize();

        // Set the destination for the NavMeshAgent to a random position with zigzag movement
        Vector3 targetPosition = transform.position + zigzagDirection * fleeDistance;

        // Sample the position to find a point on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 10f, NavMesh.AllAreas))
        {
            // Set the destination for the NavMeshAgent to the calculated position
            agent.SetDestination(hit.position);
        }
    }

    private void OnDrawGizmos()
    {
        if (state == State.Idle)
        {
            Gizmos.color = Color.green;
        }
        else if (state == State.Moving)
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
