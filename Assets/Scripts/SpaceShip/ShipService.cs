using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipService : MonoSingletonGeneric<ShipService>
{
    public ShipView ShipView;
    public ShipScriptableObjectList ShipList;
    public List<ShipController> Ships = new List<ShipController>();
    ShipController controller;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void fire(Transform bulletSpawn, float bulletDamange)
    {
        WeaponService.Instance.SpawnBullet(bulletSpawn);
    }

    public void DestroyShip(ShipController controller)
    {
        for (int i = 0; i < Ships.Count; i++)
        {
            if (controller == Ships[i])
            {
                controller.Destroy();
            }
        }
        UIService.Instance.EnableGameOverUI();
    }

    public void EnableBarrierPower()
    {
        Ships[0].EnableBarrierPower();
    }

    public void SetHealthTextValue(float health)
    {
        UIService.Instance.SetHealhTextValue(health);
    }

    public ShipView GetCurrentPlayer()
    {
        return Ships[0].GetShipView();
    }

    IEnumerator DestroySsceneObjects()
    {
        yield return new WaitForSeconds(3f);
    }

    public void SpawnShipPrefab(Transform spawner)
    {
        ShipModel model = new ShipModel(ShipList.shipScriptableObject[0]);
        ShipController Ship = new ShipController(model, ShipView, spawner);
        Ships.Add(Ship);
    }
}