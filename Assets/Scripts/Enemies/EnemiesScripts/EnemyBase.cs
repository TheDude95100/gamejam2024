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

    private Animator animator;
    private int currentHealth;


    void Start()
    {
        EnemiesManager.Instance.AddEnemy(this);

        if (groupID == -1)
        {
            int randomGroupID = Random.Range(100, 10000000);
            groupID = randomGroupID; // good enough
        }

        animator = GetComponent<Animator>();
        currentHealth = enemyData.health;
    }


    void Update()
    {
    }


    void Die()
    {
        EnemiesManager.Instance.KillEnemy(this);
        // TODO do cool stuff here
    }

    public void SetRunning()
    {
        ResetAll();
        animator.SetBool("IsRunning", true);
    }

    public void SetAttacking()
    {
        ResetAll();
        animator.SetBool("IsAttacking", true);
    }

    public void SetIdle()
    {
        ResetAll();
    }

    void ResetAll()
    {
        Debug.Log("Resetting all");
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", false);
    }

    public void SetDeath()
    {
        animator.SetTrigger("Death");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyData.visionRange);

        if (abilityData.Length == 0 || abilityIndexVisualizer >= abilityData.Length) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, abilityData[abilityIndexVisualizer].AttackRange);
    }

    internal void LockMovements()
    {
        GetComponent<MovementModule>().SetLock(true);
    }

    internal void UnlockMovements()
    {
        GetComponent<MovementModule>().SetLock(false);
    }
}
