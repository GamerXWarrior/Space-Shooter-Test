using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class AsteroidService : MonoSingletonGeneric<AsteroidService>
{
    public AsteroidView asteroidView;
    public AsteroidScriptableObjectList AsteroidList;
    public List<AsteroidController> asteroidAsteroids = new List<AsteroidController>();
    private Coroutine coroutine;
    private Vector3 SpawnerPos;
    private Quaternion SpawnerRotation;
    private int asteroidNumber;

    private AsteroidPoolService asteroidPoolService;

    protected override void Start()
    {
        base.Start();
        asteroidPoolService = GetComponent<AsteroidPoolService>();
    }

    public void SpawnAsteroid(Vector3 asteroidSpawnerPos, Quaternion asteroidSpawnerRotation, int asteroidIndex)

    {
        this.SpawnerPos = asteroidSpawnerPos;
        this.SpawnerRotation = asteroidSpawnerRotation;
        this.asteroidNumber = asteroidIndex;

        AsteroidModel model = new AsteroidModel(AsteroidList.asteroidScriptableObject[asteroidIndex]);
        AsteroidController controller = asteroidPoolService.Getasteroid(model, model.AsteroidView, asteroidSpawnerPos, asteroidSpawnerRotation, asteroidIndex);
        controller.Enable();
        asteroidAsteroids.Add(controller);
    }
   
    public void DestroyAllEnemies()
    {
        if (coroutine != null)
        {
            StopCoroutine(DestroyAllViews());
        }
        coroutine = StartCoroutine(DestroyAllViews());
    }


    IEnumerator DestroyAllViews()
    {
        AsteroidView[] asteroid = GameObject.FindObjectsOfType<AsteroidView>();
        foreach (AsteroidView asteroidAsteroid in asteroid)
        {
            yield return new WaitForSeconds(1f);
            asteroidAsteroid.Destroy();
        }
        EventService.Instance.OnGameOver();
    }

    public void DestroyAsteroid(AsteroidController controller)
    {
        for (int i = 0; i < asteroidAsteroids.Count; i++)
        {
            if (controller == asteroidAsteroids[i])
            {
                //SetasteroidCounter();
                controller.Disable();

                asteroidPoolService.ReturnItem(controller);
                asteroidAsteroids[i] = null;
            }
        }
    }

    async void SpawnAsteroidAgain(Vector3 SpawnerPos, Quaternion SpawnerRotation, int asteroidNumber)
    {
        //await new WaitForSeconds(2f);
        await Task.Delay(TimeSpan.FromSeconds(2));
        SpawnAsteroid(SpawnerPos, SpawnerRotation, asteroidNumber);
    }
}