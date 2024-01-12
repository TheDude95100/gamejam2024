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
    [SerializeField] private int currentHealth = 10;

    public void TakeDamage(int hp){
        currentHealth -= hp;
        if (currentHealth < 1) {
            Die();
        }
    }

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
        if (animator == null) return;
        ResetAll();
        animator.SetBool("IsRunning", true);
    }

    public void SetAttacking()
    {
        if (animator == null) return;
        ResetAll();
        animator.SetBool("IsAttacking", true);
    }

    public void SetAttackSpe()
    {
        if (animator == null) return;
        ResetAll();
        animator.SetBool("IsAttackSpe", true);
    }

    public void SetIdle()
    {
        if (animator == null) return;
        ResetAll();
    }

    void ResetAll()
    {
        Debug.Log("Resetting all");
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsAttackSpe", false);
    }

    public void SetDeath()
    {
        if (animator == null) return;
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
