using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPoolService : PoolingService<AsteroidController>
{

    private AsteroidModel asteroidModel;
    private AsteroidView asteroidPrefab;
    private Vector3 spawnerPos;
    private Quaternion spawnerRotation;
    private int asteroidNumber;
    private AsteroidType asteroidType;


    public AsteroidController Getasteroid(AsteroidModel asteroidModel, AsteroidView asteroidView, Vector3 spawnerPos, Quaternion spawnerRotation, int asteroidNumber)
    {
        this.asteroidModel = asteroidModel;
        this.asteroidPrefab = asteroidView;
        this.spawnerPos = spawnerPos;
        this.spawnerRotation = spawnerRotation;
        this.asteroidNumber = asteroidNumber;
        this.asteroidType = asteroidModel.AsteroidType;
        return GetItem(this.asteroidType);
    }
    protected override AsteroidController CreateItem()
    {
        AsteroidController asteroidController = new AsteroidController(asteroidModel, asteroidPrefab, spawnerPos, spawnerRotation, asteroidNumber);

        return asteroidController;
    }
}