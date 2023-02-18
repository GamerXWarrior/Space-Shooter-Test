using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidScriptableObject", menuName = "ScriptableObject/AsteroidScriptableObject")]
public class AsteroidScriptableObject : ScriptableObject
{
    public AsteroidType AsteroidType;
    [Range(2,3)]
    public int SplitsCount;
    [Range(10, 30)]
    public float AsteroidDamge;
    public int AsteroidSpeed;
    [Range(1,5)]
    public int AsteroidRotationSpeed;
    public float AsteroidHealth;
    public AsteroidView AsteroidView;
}