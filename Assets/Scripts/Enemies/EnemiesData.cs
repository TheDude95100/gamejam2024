using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData")]

public class EnemiesData : ScriptableObject
{
    [Header("Health")]
    [Range(1, 100)]
    public int health = 1;

    [Header("Speed")]
    [Range(1, 100)]
    public int speed = 1;

    [Header("Damage")]
    [Range(1, 100)]
    public int damage = 1;

    [Header("Attack Speed")]
    [Range(1, 100)]
    public int attackSpeed = 1;
}
