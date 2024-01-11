using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AbilityData", menuName ="Data/AbilityData")]
public class AbilityData : ScriptableObject
{
    [SerializeField] private string abilityName = "Punch";
    [Range(0,5)]
    [SerializeField] private float castingTime = 0.75f;
    [Range(0.5f,20f)]
    [SerializeField] private float cooldown = 2f;

    public string getAbilityName()
    {
        return abilityName;
    }

    public float getCastingTime()
    {
        return castingTime;
    }

    public float getCooldown()
    {
        return cooldown;
    }
}
