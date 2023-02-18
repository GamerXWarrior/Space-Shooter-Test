using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBySideTeleporter : MonoBehaviour
{
    [SerializeField]
    Transform targetSide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Vector2 playerPos = collision.transform.position;
            collision.transform.position = new Vector2(targetSide.position.x, playerPos.y);
        }
    }
}