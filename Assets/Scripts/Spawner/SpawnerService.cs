using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerService : MonoSingletonGeneric<SpawnerService>
{
    //private SpawnerModel[] model = new SpawnerModel[3];
    //public SpawnerView spawnerView;
    public Transform shipSpawner;
    public Transform[] enemySpawners;
    public Transform cutSceneAsteroidSpawner;
    [SerializeField]
    private int speedIncrementasteroidCount;
    [SerializeField]
    private float spawiningTimeDifference;
    private int spawnedAsteroidsCount = 0;
    private Transform enemySpawnersTransform;
    public Transform environment;
    private int childContetnt;
    private float cameraSize = 5f;
    private bool isCutScenStart = false;
    private Camera _cam;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        EventService.Instance.GameStart += OnGameStart;
        _cam = Camera.main;
        _cam.orthographicSize = 5f;
        StartCoroutine(StartCutScene());
    }

    private void OnGameStart()
    {
        InvokeRepeating(nameof(SpawnAsteroids), 2, spawiningTimeDifference);
    }

    private void SpawnAsteroids()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            SpawnAsteroids(i);
            spawnedAsteroidsCount++;
            if (spawnedAsteroidsCount == speedIncrementasteroidCount)
            {
                spawnedAsteroidsCount = 0;
                spawiningTimeDifference--;
            }
        }
    }

    IEnumerator StartCutScene()
    {
        yield return new WaitForSeconds(1f);
        SpawnShips();
        SpawnCutSceneAsteroid();
        yield return new WaitForSeconds(1f);
        isCutScenStart = true;
    }

    private void Update()
    {
        if (!isCutScenStart)
            return;
        if (isCutScenStart)
        {
            if (_cam.orthographicSize >= 10f)
            {
                isCutScenStart = false;
                EventService.Instance.OnCameraSet();
            }
            cameraSize += 2 * Time.deltaTime;
            Camera.main.orthographicSize = cameraSize;
        }



    }

    public void DestroyEverything()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return StartCoroutine(DestroyEnemies());
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(DestroyEnvironment());
    }
    IEnumerator DestroyEnemies()
    {
        yield return null;
    }

    IEnumerator DestroyEnvironment()
    {
        yield return null;
    }

    public void SpawnCutSceneAsteroid()
    {
        AsteroidService.Instance.SpawnAsteroid(cutSceneAsteroidSpawner.position, cutSceneAsteroidSpawner.rotation, 0);
    }

    public void SpawnShips()
    {
        ShipService.Instance.SpawnShipPrefab(shipSpawner);
    }
    public void SpawnAsteroids(int enemyNumber)
    {
        enemySpawnersTransform = enemySpawners[enemyNumber].transform;
        AsteroidService.Instance.SpawnAsteroid(enemySpawnersTransform.position, enemySpawnersTransform.rotation, enemyNumber);
    }
}