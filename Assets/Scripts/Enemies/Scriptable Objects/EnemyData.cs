using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName ="Data/EnemyData")]

public class EnemyData : ScriptableObject
{
    [Header("Health")]
    [Range(1, 100)]
    public int health = 1;

    [Header("Speed")]
    [Range(0, 100)]
    public int speed = 1;

    [Header("Damage")]
    [Range(0, 100)]
    public int damage = 1;

    [Header("VisionRange / Blue Sphere")]
    [Range(1, 100)]
    public int visionRange = 1;

    [Header("Attack / Red Sphere")]
    [Range(1, 100)]
    public int attackSpeed = 1;

    [Range(1, 100)]
    public int attackRange = 1;
}
