using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBase))]
public class AttackModule : MonoBehaviour
{
    private Transform player;
    private float timer;
    private float timerSpecialAttack;
    private AbilityData[] abilityData;
    private AbilityData currentAbility;
    private EnemyBase enemyBase;
    private float abilityDuration = 2f;

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
        timerSpecialAttack = 0f;

        abilityData = GetComponent<EnemyBase>().abilityData;
        state = State.Idle;

        enemyBase = GetComponent<EnemyBase>();

        currentAbility = abilityData[0];
    }


    void Update()
    {
        // TODO If player is not dead return


        timer -= Time.deltaTime;
        timerSpecialAttack -= Time.deltaTime;
        // cooldown is currentAbility.Cooldown in secondes
        if (timer > currentAbility.Cooldown + abilityDuration)
        {
            state = State.Casting;
        }
        else if (timer > abilityDuration)
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

        if (state != State.Idle && state != State.Attacking)
        {
            return;
        }

        // if player is in range, attack
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);
        float distanceZ = Mathf.Abs(player.position.z - transform.position.z);
        float distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceZ, 2));

        if (distance <= currentAbility.AttackRange)
        {
            timer = currentAbility.Cooldown + abilityDuration + currentAbility.CastingTime;
        }
    }

    void Idle()
    {
        Debug.Log("Idle fase");
        enemyBase.UnlockMovements();
        enemyBase.SetIdle();
    }

    void Caste()
    {
        Debug.Log("Casting fase");
        //enemyBase.SetIdle();

        ChooseAbility();
        // Lock movements
        enemyBase.LockMovements();
    }

    void Attack()
    {
        Debug.Log("Attacking fase");

        ChooseAbility();
        // Lock movements
        enemyBase.LockMovements();

        // Face the player
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);

        if (currentAbility == abilityData[0])
        {
            enemyBase.SetAttacking();
        }
        else
        {
            enemyBase.SetAttackSpe();
        }
    }

    void Cooldown()
    {
        Debug.Log("Cooldown fase");
        enemyBase.UnlockMovements();
        //enemyBase.SetIdle();
    }

    void ChooseAbility()
    {
        Debug.Log("ChooseAbility");
        int randomIndex = Random.Range(0, abilityData.Length);
        currentAbility = abilityData[randomIndex];

        /*
        if (abilityData.Length == 1)
        {
            currentAbility = abilityData[0];
            return;
        }   

        Debug.Log("ChooseAbility : " + timerSpecialAttack);
        if (timerSpecialAttack < 0f)
        {
            currentAbility = abilityData[1];
        }
        else
        { 
            currentAbility = abilityData[0];
        }
        */
    }


    public Transform GetPlayerTransform()
    {
        return player;
    }
}
