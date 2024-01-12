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
    private EnemyBase enemyBase;

    private enum State
    {
        Idle,
        Casting,
        Attacking,
        Cooldown
    }
    private State state;
    private State previousState;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        timer = 0f;

        abilityData = GetComponent<EnemyBase>().abilityData;
        state = State.Idle;

        enemyBase = GetComponent<EnemyBase>();

        ChooseRandomAbility();
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

        if (state != previousState)
        {
            switch (state)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Casting:
                    Caste();
                    break;
                case State.Attacking:
                    Attack();
                    break;
                case State.Cooldown:
                    Cooldown();
                    break;
            }
        }

        previousState = state;

        if (state != State.Idle)
        {
            return;
        }

        // if player is in range, attack
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);
        float distanceZ = Mathf.Abs(player.position.z - transform.position.z);
        float distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2));

        if (distance <= currentAbility.AttackRange)
        {
            timer = currentAbility.Cooldown + currentAbility.Duration + currentAbility.CastingTime;
        }
    }

    void Idle()
    {
        Debug.Log("Idle fase");
        enemyBase.SetIdle();
    }

    void Caste()
    {
        Debug.Log("Casting fase");
        enemyBase.SetIdle();
        ChooseRandomAbility();
        // Lock movements
        enemyBase.LockMovements();
    }

    void Attack()
    {
        Debug.Log("Attacking fase");
        // Face the player
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);

        enemyBase.SetAttacking();
    }

    void Cooldown()
    {
        Debug.Log("Cooldown fase");
        enemyBase.UnlockMovements();
        enemyBase.SetIdle();
    }

    void ChooseRandomAbility()
    {
        int randomIndex = Random.Range(0, abilityData.Length);
        currentAbility = abilityData[randomIndex];
    }


    public Transform GetPlayerTransform()
    {
        return player;
    }
}
