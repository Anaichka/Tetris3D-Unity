using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    int score;
    int level;
    int layersCleared;

    bool gameIsOver = false;
    
    float fallSpeed;

    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetScore(score);
    }

    public void SetScore(int amount)
    {
        score += amount;
        CalculateLevel();
        UIHandler.instance.UpdateUI(score, level, layersCleared);

        // update UI
    }

    public float ReadFallSpeed()
    {
        return fallSpeed;
    }

    public void LayersCleared(int amount)
    {
        if (amount == 1)
        {
            SetScore(400);
        } 
        else if (amount == 2)
        {
            SetScore(1000);
        }
        else if (amount == 2)
        {
            SetScore(2500);
        }
        else if (amount == 2)
        {
            SetScore(4000);
        }
        else if (amount == 2)
        {
            SetScore(6000);
        }
        else if (amount == 2)
        {
            SetScore(8000);
        }
        else if (amount == 2)
        {
            SetScore(10500);
        }


        layersCleared += amount;
        UIHandler.instance.UpdateUI(score, level, layersCleared);
        // update UI
    }

    void CalculateLevel()
    {
        if (score <= 10000)
        {
            level = 1;
            fallSpeed = 1.2f;
        } 
        else if (score > 10000 && score <= 20000)
        {
            level = 2;
            fallSpeed = 1.1f;
        }
        else if (score > 0000 && score <= 30000)
        {
            level = 3;
            fallSpeed = 1f;
        }
        else if (score > 30000 && score <= 40000)
        {
            level = 4;
            fallSpeed = 0.8f;
        }
        else if (score > 50000 && score <= 60000)
        {
            level = 5;
            fallSpeed = 0.6f;
        }
        else if (score > 60000 && score <= 70000)
        {
            level = 6;
            fallSpeed = 0.5f;
        }
        else
        {
            level = 7;
            fallSpeed = 0.7f;
        }
    }

    public bool ReadGameIsOver()
    {
        return gameIsOver;
    }

    public void SetGameIsOver()
    {
        gameIsOver = true;
        UIHandler.instance.SetGOWindow();
    }
}
