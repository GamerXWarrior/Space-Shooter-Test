using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesService : MonoSingletonGeneric<CollectiblesService>
{

    [SerializeField]
    private GameObject[] powerUps;

    [Range(5,10)]
    [SerializeField]
    private int powerUpSpawingTimeDifference;

    [Range(-8, 8)]
    [SerializeField]
    private int powerupSpawinigRadius;

    protected override void Start()
    {
        EventService.Instance.GameStart += StartCollectiblesSpawninig;
    }

    private void StartCollectiblesSpawninig()
    {
        InvokeRepeating(nameof(SpawnCollectibles), 10, powerUpSpawingTimeDifference);
    }

    private void SpawnCollectibles()
    {
        int powerUpIndex = Random.Range(0, powerUps.Length);
        Debug.Log("power index: " + powerUps.Length);
        Vector2 spawningPos = new Vector2(Random.Range(0, powerupSpawinigRadius), Random.Range(0, powerupSpawinigRadius));
        GameObject powerUp = Instantiate(powerUps[powerUpIndex], spawningPos, transform.rotation);
    }
}