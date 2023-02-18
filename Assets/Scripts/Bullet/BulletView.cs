
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletView : MonoBehaviour
{
    private int bulletSpeed;
    public Rigidbody2D rb;
    private float damage;
    private BulletController controller;
    private BulletModel model;
    private Enum bulletType;

    private float initDamage;

    private void Start()
    {

    }

    public void SetBulletDetails(float healthDamage, Enum _bulletType)
    {
        model = controller.BulletModel;
        bulletSpeed = model.Speed;
        bulletType = _bulletType;
       
        damage = healthDamage;
    }

    public Enum GetBulletType()
    {
        return bulletType;
    }

    public void InitializeController(BulletController bulletController)
    {
        controller = bulletController;
    }

    public BulletController GetController()
    {
        return controller;
    }


    public void Destroy()
    {
        Destroy(gameObject, 0.1f);
    }

    public void Enable(Transform spawner, int angle)
    {
        transform.localPosition = spawner.position;
       
        transform.rotation = spawner.rotation;
        transform.Rotate(0, 0, angle);

        rb.WakeUp();
        gameObject.SetActive(true);
        rb.velocity = transform.up * bulletSpeed;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        rb.Sleep();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.GetComponent<BulletView>() && !collision.transform.GetComponent<ShipView>())
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                controller.DestroyBulletView();
                damagable.TakeDamage(damage);
            }
        }
    }
}