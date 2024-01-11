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

    [Header("Obstacle Avoidance")]
    [Range(0, 10)]
    public float minRandomRadiusAvoidanceRange = 0.5f;
    [Range(0, 10)]
    public float maxRandomRadiusAvoidanceRange = 1f;

    public bool targetSurrounding = true;




    [Header("Damage")]
    [Range(0, 100)]
    public int damage = 1;


    [Header("VisionRange / Blue Sphere")]
    [Range(1, 100)]
    public int visionRange = 1;


    [Header("Attack / Red Sphere")]
    [Range(1, 100)]
    public int attackRange = 1;
    [Range(1, 100)]
    public int attackCooldown = 1;

    [Header("Additional Parameters")]
    bool roaming = false;
}
