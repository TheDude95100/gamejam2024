using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance;
    public GameObject orb;
    private List<EnemyBase> enemies = new List<EnemyBase>();

    void Awake()
    {
        Instance = this;
    }
    public void AddEnemy(EnemyBase enemy)
    {
        Debug.Log("Added enemy : " + enemy.name);
        enemies.Add(enemy);
    }

    public void KillEnemy(EnemyBase enemy)
    {
        if (orb){
            GameObject orb_instantiate = Instantiate(orb,enemy.transform.position, Quaternion.identity);
            orb_instantiate.SetActive(true);
        }
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    internal void NoticePlayerDetected(int groupID)
    {
        foreach (EnemyBase enemy in enemies)
        {
            if (enemy.groupID == groupID)
            {
                enemy.GetComponent<MovementModule>().PlayerDetected();
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
            enemy.GetComponent<MovementModule>().GroupHasLostPlayer();
        }
    }
}
