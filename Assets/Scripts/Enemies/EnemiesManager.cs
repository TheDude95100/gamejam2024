using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance;
    private List<EnemyBase> enemies = new List<EnemyBase>();


    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        
    }

    public void AddEnemy(EnemyBase enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyBase enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
