using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBulletPower : MonoBehaviour
{
    [SerializeField]
    private int impactTime;

    [SerializeField]
    private WeaponType desiredWeaponType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            WeaponService.Instance.SetCurrentWeaponTemporary(desiredWeaponType, impactTime);
            Destroy(gameObject);
        }
    }
}
