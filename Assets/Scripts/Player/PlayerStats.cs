using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Sarting stats")]
    [SerializeField] private int maxLife = 20;
    [SerializeField] private int defense = 0;

    [SerializeField] private float basicAttackDamage = 2;
    [SerializeField] private float heavyStrikeDamage = 3;
    [SerializeField] private float whirlwindDamage = 2;

    private bool isAlive;
    private int level;
    private float currentLife;
    private int currentExp;
    private int expNextLevel;

    private float bonusAttackSpeed;
    private float bonusMovementSpeed;
    private int bonusExpGain;


    public int MaxLife => maxLife;
    public int Defense => defense;

    public float BasicAttackDamage => basicAttackDamage;
    public float HeavyStrikeDamage => heavyStrikeDamage;
    public float WhirlwindDamage => whirlwindDamage;

    public int Level => level;
    public float CurrentLife => currentLife;
    public int CurrentExp => currentExp;
    public int ExpNextLevel => expNextLevel;

    public float BonusAttackSpeed => bonusAttackSpeed;
    public float BonusMovementSpeed => bonusMovementSpeed;
    public int BonusExpGain => bonusExpGain;


    private void Start()
    {
        isAlive = true;
        currentLife = maxLife;
    }

    public void AddBasicAttackDamage(float modifier)
    {
        basicAttackDamage += modifier;
    }

    public void AddHeavyStrikeDamage(float modifier)
    {
        heavyStrikeDamage += modifier;
    }

    public void AddWhirlwindDamage(float modifier)
    {
        whirlwindDamage += modifier;
    }

    public void AddDefense(int modifier)
    {
        defense += modifier;
    }

    public void AddBonusAttackSpeed(float modifier)
    {
        bonusAttackSpeed += modifier;
    }

    public void AddBonusMovementSpeed(float modifier)
    {
        bonusMovementSpeed += modifier;
    }

    public void AddBonusExpGain(int modifier)
    {
        bonusExpGain += modifier;
    }

    public void IncreaseMaxLife(int modifier)
    {
        maxLife += modifier;
    }

    public void GainExperience(int modifier)
    {
        if(currentExp + modifier < ExpNextLevel)
        {
            currentExp += modifier + BonusExpGain;
        }
        else
        {
            currentExp = currentExp + modifier + BonusExpGain - ExpNextLevel;
            LevelUP();
        }
    }
    
    private void LevelUP()
    {
        level += 1;
        IncreaseMaxLife(3);
        HealDamage(MaxLife);
    }

    public void DealtDamage(float damage)
    {
        if(damage - Defense <= 0)
        { 
            return;
        }

        if(CurrentLife - (damage - Defense) > 0)
        {
            currentLife -= damage;
        }
        else
        {
            currentLife = 0;
            isAlive = false;
        }
    }

    public void HealDamage(float healing)
    {
        if (currentLife + healing >= MaxLife)
        {
            currentLife = MaxLife;
        }
        else
        {
            currentLife += healing;
        }
    }
}
