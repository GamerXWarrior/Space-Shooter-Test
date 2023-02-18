using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletService : MonoSingletonGeneric<BulletService>
{
    public BulletScriptableObjectList bulletList;
    public List<BulletController> bullets = new List<BulletController>();
   
    private float bulletDamage;
    private BulletPoolService bulletPoolService;
    protected override void Awake()
    {
        bulletPoolService = GetComponent<BulletPoolService>();
        base.Awake();
    }

    public BulletController SpawnBullet(Transform bulletSpawner, WeaponScriptableObject currentWeapon, int angle)
    {

        BulletModel bulletModel = new BulletModel(currentWeapon);
        BulletController controller = bulletPoolService.GetBullet(bulletModel, bulletModel.BulletView, bulletSpawner, bulletDamage);
        controller.Enable(bulletSpawner, angle);
        bullets.Add(controller);
        return controller;
    }
    
    public void DestroyView(BulletController controller)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (controller == bullets[i])
            {
                controller.Disable();
                bulletPoolService.ReturnItem(controller);
                bullets[i] = null;
            }
        }
    }

}
