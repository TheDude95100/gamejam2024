using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int groupID = 0;

    // Start is called before the first frame update
    void Start()
    {
        EnemiesManager.Instance.AddEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die()
    {
        EnemiesManager.Instance.RemoveEnemy(this);
    }
}
