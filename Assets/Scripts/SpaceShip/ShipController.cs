using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController
{
    public ShipController(ShipModel shipModel, ShipView shipPrefab, Transform spawner)
    {
        ShipModel = shipModel;
        ShipView = GameObject.Instantiate<ShipView>(shipPrefab, spawner.transform.position, spawner.transform.rotation);
        ShipView.InitialiseController(this);
        ShipView.SetViewDetails();
    }


    public void fire(Transform bulletSpawn, float bulletDamange)
    {
        ShipService.Instance.fire(bulletSpawn, bulletDamange);
    }

    public void ApplyDamage(float damage)
    {
        if (ShipModel != null)
        {
            if ((ShipModel.Health - damage) <= 0)
            {
                DestroyView();
            }
            else
            {
                ShipModel.Health -= damage;
                ShipView.SetShipHealth(ShipModel.Health);
            }
        }
        return;
    }

    public void SetShipHealth(float health)
    {
        ShipService.Instance.SetHealthTextValue(health);
    }

    public void EnableBarrierPower()
    {
        ShipView.EnableBarrierPowerUp();
    }
    public void DestroyView()
    {
        ShipService.Instance.DestroyShip(this);
    }
    public void Destroy()
    {
        if (ShipView != null && ShipModel != null)
        {
            ShipView.Destroy();
            ShipModel = null;
        }
        return;
    }

    public ShipView GetShipView()
    {
        return ShipView.GetView();
    }

    public ShipModel ShipModel { get; set; }
    public ShipView ShipView { get; }
}