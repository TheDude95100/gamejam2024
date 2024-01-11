using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBase))]
public class AttackModule : MonoBehaviour
{
    private Transform player;
    private float timer;
    private AbilityData[] abilityData;
    private AbilityData currentAbility;

    private enum State
    {
        Idle,
        Casting,
        Attacking,
        Cooldown
    }
    private State state;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        timer = 0f;

        abilityData = GetComponent<EnemyBase>().abilityData;
        state = State.Idle;
    }


    void Update()
    {
        // TODO If player is not dead return

        timer -= Time.deltaTime;
        if (timer > currentAbility.Cooldown + currentAbility.Duration)
        {
            state = State.Casting;
        }
        else if (timer > currentAbility.Duration)
        {
            state = State.Attacking;
        }
        else if (timer > 0f)
        {
            state = State.Cooldown;
        }
        else
        {
            state = State.Idle;
        }

        if (state != State.Idle)
        {
            return;
        }

        // if player is in range, attack
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);
        float distanceZ = Mathf.Abs(player.position.z - transform.position.z);
        float distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2));

        ChooseRandomAbility();
        if (distance <= currentAbility.AttackRange)
        {
            timer = currentAbility.Cooldown + currentAbility.Duration + currentAbility.CastingTime;
        }
    }

    void Attack()
    {
        // TODO do stuff here
    }

    void ChooseRandomAbility()
    {
        int randomIndex = Random.Range(0, abilityData.Length);
        currentAbility = abilityData[randomIndex];
    }
}
