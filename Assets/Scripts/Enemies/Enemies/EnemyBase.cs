using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public int groupID = 0;
    public EnemyData enemyData;
    public AbilityData[] abilityData;
    public int abilityIndexVisualizer = 0;


    void Start()
    {
        EnemiesManager.Instance.AddEnemy(this);

        if (groupID == -1)
        {
            int randomGroupID = Random.Range(100, 10000000);
            groupID = randomGroupID; // good enough
        }
    }


    void Update()
    {
    }


    void Die()
    {
        EnemiesManager.Instance.KillEnemy(this);
        // TODO do cool stuff here
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyData.visionRange);

        if (abilityData.Length == 0 || abilityIndexVisualizer >= abilityData.Length) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, abilityData[abilityIndexVisualizer].AttackRange);
    }
}
