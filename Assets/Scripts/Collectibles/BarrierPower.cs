using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierPower : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("PlayerDetected");
            ShipService.Instance.EnableBarrierPower();
            Destroy(gameObject);
        }
    }
}
