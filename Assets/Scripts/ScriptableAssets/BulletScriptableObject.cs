using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObject/BulletScriptableObject")]
public class BulletScriptableObject : ScriptableObject
{

    public WeaponType BulletWeaponType;
    public int bulletSpeed;
    public int Damage;
    public BulletView bulletView;

}

