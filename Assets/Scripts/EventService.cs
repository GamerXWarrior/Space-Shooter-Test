using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventService : MonoSingletonGeneric<EventService>
{
    public event Action PlayerSpawn;
    public event Action GameStart;
    public event Action GameOver;
    public event Action CameraSet;
  

    public void OnPlayerSpawn()
    {
        PlayerSpawn?.Invoke();
    }
    
    public void OnGameStart()
    {
        GameStart?.Invoke();
    }
    
    public void OnCameraSet()
    {
        CameraSet?.Invoke();
    }

    public void OnGameOver()
    {
        GameOver?.Invoke();
    }
}