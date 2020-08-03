using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;
    
    public Text scoreText;
    public Text levelText;
    public Text layersText;

    public GameObject gameOverWindow;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOverWindow.SetActive(false);
    }

    public void UpdateUI(int score, int level, int layers)
    {
        scoreText.text = "Score: " + score.ToString("D9");
        levelText.text = "Level: " + level.ToString("D2");
        layersText.text = "Layers: " + layers.ToString("D9");
    }

    public void SetGOWindow()
    {
        gameOverWindow.SetActive(true);
    }
}
