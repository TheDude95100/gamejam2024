using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : EnemyBase
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is reserved for mother class, do not override it

    // Update is called once per frame
    void Update()
    {
        
    }
}
