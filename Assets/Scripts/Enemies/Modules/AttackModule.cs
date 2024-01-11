using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBase))]
public class AttackModule : MonoBehaviour
{
    private Transform player;
    private float timer;
    private EnemyData enemyData;

    public enum Attacks
    {
        Attack1,
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        timer = 0f;

        enemyData = GetComponent<EnemyBase>().enemyData;
    }


    void Update()
    {
        // TODO If player is not dead return

        // if timer is not 0, increment it
        timer -= Time.deltaTime;
        if (timer != 0f)
        {
            return;
        }

        // if player is in range, attack
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);
        float distanceZ = Mathf.Abs(player.position.z - transform.position.z);
        float distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2));

        if (distance <= enemyData.attackCooldown)
        {
            Attack(Attacks.Attack1);
            timer = enemyData.attackCooldown;
        }
    }

    void Attack(Attacks attack)
    {
    }
}
