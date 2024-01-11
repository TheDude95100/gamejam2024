using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public int groupID = 0;
    public EnemyData enemyData;

    private NavMeshAgent agent;
    private Transform target;


    void Start()
    {
        EnemiesManager.Instance.AddEnemy(this);
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
    }


    void Update()
    {
        agent.SetDestination(target.position);
    }


    void Die()
    {
        EnemiesManager.Instance.KillEnemy(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyData.visionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.attackRange);
    }
}
