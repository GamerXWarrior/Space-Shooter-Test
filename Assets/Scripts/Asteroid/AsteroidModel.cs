using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidModel
{
    public AsteroidModel(AsteroidScriptableObject enemy)
    {
        AsteroidType = enemy.AsteroidType;
        EnemySpeed = enemy.AsteroidSpeed;
        EnemyRotation = enemy.AsteroidRotationSpeed;
        EnemyHealth = enemy.AsteroidHealth;
        EnemyDamage = enemy.AsteroidDamge;
        AsteroidView = enemy.AsteroidView;
        SplitsCount = enemy.SplitsCount;
    }

    public AsteroidType AsteroidType { get; }
    public AsteroidView AsteroidView { get; }
    public int EnemySpeed { get; }
    public int SplitsCount { get; }
    public int EnemyRotation { get; }
    public float EnemyHealth { get; set; }
    public float EnemyDamage { get; }
}