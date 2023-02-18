using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObject/WeaponScriptableObject")]
public class WeaponScriptableObject : ScriptableObject
{
    public WeaponType weaponType;
    public BulletScriptableObject bullet;
    
    [Range(1, 5)]
    public int bulletCounts;

    [Range(0,20)]
    public int angle;
}
