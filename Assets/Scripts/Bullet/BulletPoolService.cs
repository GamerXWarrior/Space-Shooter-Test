using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BulletPoolService : PoolingService<BulletController>
{
    private BulletModel model;
    private BulletView bulletPrefab;
    private Transform spawner;
    private BulletType bulletWeaponType;
    private float damage;

    public BulletController GetBullet(BulletModel bulletModel, BulletView bulletView, Transform spawner, float damageValue)
    {
        this.model = bulletModel;
        this.bulletPrefab = bulletView;
        this.spawner = spawner;
        this.damage = bulletModel.Damage;
        this.bulletWeaponType = bulletModel.BulletType;
        return GetItem(bulletWeaponType);
    }

    protected override BulletController CreateItem()
    {
        BulletController bulletController = new BulletController(model, bulletPrefab, spawner, damage);
        return bulletController;
    }
}
