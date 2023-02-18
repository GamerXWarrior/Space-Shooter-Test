using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletController
{
    public BulletController(BulletModel bulletModel, BulletView bulletView, Transform spawner, float damageValue)
    {
        BulletModel = bulletModel;
        Spawner = spawner;
        DamageValue = damageValue;
        BulletView = GameObject.Instantiate(bulletView, spawner.transform.position, spawner.transform.rotation);
      
        BulletView.InitializeController(this);

        BulletView.SetBulletDetails(DamageValue, BulletModel.BulletType);
    }

    public void DestroyBulletView()
    {
        if (BulletView != null)
        {
            BulletService.Instance.DestroyView(this);
        }
    }

    public void Destroy()
    {
        if (BulletView != null)
        {
            BulletModel = null;
            BulletView.Destroy();
        }
    }
    public void Disable()
    {
        BulletView.Disable();
    }
    public void Enable(Transform spawner, int angle)
    {
        BulletView.Enable(spawner, angle);
    }

    public BulletModel BulletModel { get; set; }
    public Transform Spawner { get; }
    public float DamageValue { get; }
    public BulletView BulletView { get; }
}