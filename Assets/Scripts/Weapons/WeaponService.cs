using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WeaponService : MonoSingletonGeneric<WeaponService>
{
    public WeaponScriptableObjectList weaponsList;
    public Dropdown weaponDropDown;
   
    private int playerDeathCounter = 0;
    private WeaponType currentWeaponType;
    private WeaponScriptableObject currentWeapon;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        SetCurrentWeaponType(WeaponType.Basic);
        weaponDropDown.onValueChanged.AddListener((Dropdown) => { SetCurrentWeaponType(weaponDropDown); });
    }

    public void SpawnBullet(Transform bulletSpawn)
    {
        if (currentWeapon.bulletCounts > 1)
        {
            int angle = currentWeapon.angle;
            int bulletsFactor = 0;
            if (currentWeapon.bulletCounts % 2 == 1)
            {
                bulletsFactor = currentWeapon.bulletCounts - 1;
            }
            else
            {
                bulletsFactor = currentWeapon.bulletCounts;
            }

            angle = angle * (bulletsFactor) / 2;
            for (int i = 0; i < currentWeapon.bulletCounts; i++)
            {
                BulletService.Instance.SpawnBullet(bulletSpawn, currentWeapon, angle);
                angle = (angle - currentWeapon.angle);
            }
        }
        else
        {
            BulletService.Instance.SpawnBullet(bulletSpawn, currentWeapon, currentWeapon.angle);
        }
    }

    public void SetCurrentWeaponType(WeaponType weaponType)
    {
        for (int i = 0; i < weaponsList.weaponScriptableObject.Length; i++)
        {
            if (weaponsList.weaponScriptableObject[i].weaponType == weaponType)
            {
                currentWeaponType = weaponType;
                currentWeapon = weaponsList.weaponScriptableObject[i];
            }
        }

        StopCoroutine("ResetWeaponType");
    }

    public void SetCurrentWeaponTemporary(WeaponType weaponType, int impactTime)
    {
        WeaponType previousWeaponType = currentWeapon.weaponType;

        SetCurrentWeaponType(weaponType);

        StartCoroutine(ResetWeaponType(previousWeaponType, impactTime));
    }

    IEnumerator ResetWeaponType(WeaponType weaponType, int impactTime)
    {
        yield return new WaitForSeconds(impactTime);
        SetCurrentWeaponType(weaponType);
    }

    public void SetCurrentWeaponType(Dropdown dropDown)
    {
        switch (dropDown.value)
        {
            case 0:
                currentWeaponType = WeaponType.Basic;
                break;

            case 1:
                currentWeaponType = WeaponType.Medium;
                break;

            case 2:
                currentWeaponType = WeaponType.Advance;
                break;

            case 3:
                currentWeaponType = WeaponType.PowerUp;
                break;
            default:
                currentWeaponType = WeaponType.Basic;
                break;
        }

        for (int i = 0; i < weaponsList.weaponScriptableObject.Length; i++)
        {
            if (weaponsList.weaponScriptableObject[i].weaponType == currentWeaponType)
            {
                currentWeaponType = weaponsList.weaponScriptableObject[i].weaponType;
                currentWeapon = weaponsList.weaponScriptableObject[i];
            }
        }
        StopCoroutine("ResetWeaponType");
    }
}