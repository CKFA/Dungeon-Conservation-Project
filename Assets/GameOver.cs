﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text waveText;
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;
    private void OnEnable()
    {
        waveText.text = PlayerStats.waves.ToString();        
    }

    public void Retry()
    {
        FindObjectOfType<PlayerStats>().Initialisation();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name,false);
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName,false);
    }
}
