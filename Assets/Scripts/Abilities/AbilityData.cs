using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AbilityData", menuName ="Data/AbilityData")]
public class AbilityData : ScriptableObject
{
    [SerializeField] 
    private string abilityName = "Punch";
    [SerializeField]
    private Sprite abilityIcon;
    [SerializeField]
    [TextArea()]
    private string description = "Punch the enemy in the face.";
    [SerializeField]
    private bool isUnlockedAbility = true;

    [SerializeField]
    private bool isActiveAbility = true;

    [SerializeField]
    [Range(0f, 100f)]
    private float baseDamage = 3f;
    [SerializeField]
    [Range(0,5)] 
    private float castingTime = 0.75f;
    [SerializeField]
    [Range(0f,20f)]
    private float cooldown = 6f;
    [SerializeField]
    [Range(100f, 200f)]
    [Tooltip("Percent of the original duration.")]
    private float duration = 100f;
    [SerializeField]
    [Range(0f, 100f)]
    private float attackRange = 3f;

    [SerializeField]
    [Tooltip("Does it gives passive bonuses?")]
    private bool hasPassiveBonus = false;

    [SerializeField]
    [Range(0f, 100f)]
    private float bonusLife = 3f;
    [SerializeField]
    [Range(0f, 100f)]
    private float bonusDamage = 3f;
    [SerializeField]
    [Range(0f, 100f)]
    private int bonusDefense = 3;
    [SerializeField]
    [Range(0f, 1f)]
    private float bonusAttackSpeed = 0.25f;
    [SerializeField]
    [Range(0f, 1f)]
    private float bonusDuration = 0.25f;
    [SerializeField]
    [Range(0f, 1f)]
    private float bonusMovementSpeed = 0.25f;
    [SerializeField]
    [Range(0f, 100f)]
    private int bonusExperienceGain = 3;

    public string AbilityName => abilityName; 
    public string Description => description;
    public bool IsUnlockedAbility => isUnlockedAbility;

    public bool IsActiveAbility => isActiveAbility;

    public float BaseDamage => baseDamage;
    public float CastingTime => castingTime;
    public float Cooldown => cooldown;
    public float Duration => duration;
    public float AttackRange => attackRange;

    public bool HasPassiveBonus => hasPassiveBonus;

    public float BonusLife => bonusLife;
    public float BonusDamage => bonusDamage;
    public int BonusDefense => bonusDefense;
    public float BonusAttackSpeed => bonusAttackSpeed;
    public float BonusDuration => bonusDuration;
    public float BonusMovementSpeed => bonusMovementSpeed;
    public int BonusExperienceGain => bonusExperienceGain;
}
