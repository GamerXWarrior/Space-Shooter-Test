using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModel
{
    public BulletModel(WeaponScriptableObject weapon)
    {
        Speed = weapon.bullet.bulletSpeed;

        switch (weapon.weaponType)
        {
            case WeaponType.None:
                BulletType = BulletType.None;
                break;
            case WeaponType.Basic:
                BulletType = BulletType.BasicBullet;
                break;
            case WeaponType.Medium:
                BulletType = BulletType.MediumBullet;
                break;
            case WeaponType.Advance:
                BulletType = BulletType.AdvanceBullet;
                break;
            case WeaponType.PowerUp:
                BulletType = BulletType.PowerUpBullet;
                break;
            default:
                break;
        }

        Damage = weapon.bullet.Damage;
        BulletView = weapon.bullet.bulletView;
    }

    public BulletView BulletView { get; }
    public int Speed { get; }
    public float Damage { get; }
    public BulletType BulletType { get; }
}