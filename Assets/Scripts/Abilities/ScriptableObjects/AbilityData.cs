using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AbilityData", menuName ="Data/AbilityData")]
public class AbilityData : ScriptableObject
{
    [SerializeField] private string abilityName;
    [SerializeField] private float castingTime;
    [SerializeField] private float cooldown;

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
