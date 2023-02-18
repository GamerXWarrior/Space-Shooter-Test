using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController
{
    public AsteroidController(AsteroidModel enemyModel, AsteroidView asteroidView, Vector3 spawnerPos, Quaternion spawnerRotation, int enemyNumber)
    {
        AsteroidModel = enemyModel;
        AsteroidHealth = AsteroidModel.EnemyHealth;
        SpawnerPos = spawnerPos;
        SpawnerRotation = spawnerRotation;
        EnemyNumber = enemyNumber;
        AsteroidView = GameObject.Instantiate<AsteroidView>(asteroidView, spawnerPos, spawnerRotation);
        AsteroidView.InitializeController(this);
        AsteroidView.SetViewDetails();
        MaxSplitsCount = enemyModel.SplitsCount;
        AsteroidViewsList = new List<AsteroidView>();
    }

    public void ApplyDamage(float damage, int splitCounter, Vector2 currentPos, Quaternion currentRot, AsteroidView _asteroidView)
    {
        if (AsteroidModel != null)
        {

            if (splitCounter <= MaxSplitsCount)
            {
                splitCounter++;
                for (int i = 0; i < 2; i++)
                {
                    AsteroidView asteroidView = GameObject.Instantiate(_asteroidView, currentPos, currentRot);

                    Vector2 steroidScale = AsteroidView.transform.localScale;
                    float xScale = (steroidScale.x) / splitCounter;
                    float yScale = (steroidScale.y) / splitCounter;
                    asteroidView.transform.localScale = new Vector2(xScale, yScale);
                  
                    AsteroidViewsList.Add(asteroidView);
                    int listIndex = AsteroidViewsList.IndexOf(asteroidView);
                    asteroidView.InitializeController(this);
                    asteroidView.SetSplitViewDetails(splitCounter, listIndex);
                }
            }
            else
            {
                UIService.Instance.SetScoreTextValue(1);
            }
            if (splitCounter > 2)
            {
                AsteroidViewsList[_asteroidView.GetListIndex()].Destroy();
            }
            if (AsteroidView.gameObject.activeSelf)
            {
                AsteroidView.Disable();
            }

            if (AsteroidViewsList.Count == 0)
            {
                DestroyView();
            }
          
        }
    }

    public void DestroyView()
    {
        AsteroidService.Instance.DestroyAsteroid(this);
    }

    public void Destroy()
    {
        if (AsteroidView != null && AsteroidModel != null)
        {
            AsteroidView.Destroy();
            AsteroidModel = null;
        }
    }
    public void Disable()
    {
        AsteroidView.Disable();
    }
    public void Enable()
    {
        AsteroidView.Enable();
        AsteroidView.SetViewDetails();
        AsteroidHealth = AsteroidModel.EnemyHealth;
        AsteroidView.SetAsteroidHealth(AsteroidHealth);
    }

    public AsteroidView AsteroidView { get; }
    public List<AsteroidView> AsteroidViewsList { get; set; }
    public AsteroidModel AsteroidModel { get; set; }
    public float AsteroidHealth { get; set; }
    public Vector3 SpawnerPos { get; }
    public Quaternion SpawnerRotation { get; }
    public int EnemyNumber { get; }
    public int MaxSplitsCount { get; }
}