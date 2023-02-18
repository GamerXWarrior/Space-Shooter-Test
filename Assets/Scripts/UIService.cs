using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIService : MonoSingletonGeneric<UIService>
{
    [SerializeField]
    Text scoreText;
    
    [SerializeField]
    Text healthText;
    
    [SerializeField]
    GameObject gameOverUI;

    private int score = 0;
    private float health = 0;

    protected override void Start()
    {
        SetScoreTextValue(score);
    }

    public void SetScoreTextValue(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    } 
    
    public void SetHealhTextValue(float value)
    {
        healthText.text = "Health: " + value;
    }

    public void EnableGameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}