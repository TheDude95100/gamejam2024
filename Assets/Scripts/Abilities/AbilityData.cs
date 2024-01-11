using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AbilityData", menuName ="Data/AbilityData")]
public class AbilityData : ScriptableObject
{
    [SerializeField] 
    private string abilityName = "Punch";
    [SerializeField]
    private bool isUnlockedAbility = true;

    [SerializeField]
    private bool activeAbility = true;

    [SerializeField]
    [Range(0,5)] 
    private float castingTime = 0.75f;
    [SerializeField]
    [Range(1.5f,20f)]
    private float cooldown = 6f;
    [SerializeField]
    [Range(0f, 100f)]
    private float baseDamage = 3f;

    [SerializeField]
    [Range(1f, 100f)]
    private float bonusDamage = 3f;
    [SerializeField]
    [Range(1f, 100f)]
    private float bonusAttackSpeed = 3f;
    [SerializeField]
    [Range(1f, 100f)]
    private float bonusLife = 3f;
    [SerializeField]
    [Range(1f, 100f)]
    private float bonusMovementSpeed = 3f;

    public string AbilityName => abilityName;
    public bool IsUnlockedAbility => isUnlockedAbility;

    public bool ActiveAbility => activeAbility;

    public float CastingTime => castingTime;
    public float Cooldown => cooldown;
    public float BaseDamage => baseDamage;

    public float BonusDamage => bonusDamage;
    public float BonusAttackSpeed => bonusAttackSpeed;
    public float BonusLife => bonusLife;
    public float BonusMovementSpeed => bonusMovementSpeed;
}
