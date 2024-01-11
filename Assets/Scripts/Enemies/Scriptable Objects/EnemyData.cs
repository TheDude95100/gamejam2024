using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName ="Data/EnemyData")]

public class EnemyData : ScriptableObject
{
    [Header("Health")]
    [Range(1, 100)]
    public int health = 1;


    [Header("Steering")]
    [Range(1, 100)]
    public float speed = 3.5f;
    [Range(1, 400)]
    public float angularSpeed = 120;
    [Range(1, 100)]
    public float acceleration = 8;
    [Range(1, 100)]
    public float stoppingDistance = 1;


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

    [Header("Additional Parameters")]
    bool roaming = false;
}
