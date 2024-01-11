using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance;
    private List<EnemyBase> enemies = new List<EnemyBase>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void AddEnemy(EnemyBase enemy)
    {
        Debug.Log("Added enemy : " + enemy.name);
        enemies.Add(enemy);
    }

    public void KillEnemy(EnemyBase enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    internal void NoticePlayerDetected(int groupID)
    {
        foreach (EnemyBase enemy in enemies)
        {
            if (enemy.groupID == groupID)
            {
                enemy.GetComponent<MovementModule>().groupHasDetectedPlayer = true;
            }
        }
    }

    internal void NoticePlayerLost(int groupID)
    {
        List<EnemyBase> enemiesGroup = new List<EnemyBase>();
        foreach (EnemyBase enemy in enemies)
        {
            if (enemy.groupID == groupID)
            {
                enemiesGroup.Add(enemy);
            }
        }

        foreach (EnemyBase enemy in enemiesGroup)
        {
            if (!enemy.GetComponent<MovementModule>().HasLostPlayer())
            {
                return;
            }
        }
        
        foreach (EnemyBase enemy in enemiesGroup)
        {
            enemy.GetComponent<MovementModule>().groupHasDetectedPlayer = false;
        }
    }
}
