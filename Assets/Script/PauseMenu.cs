using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;
    public GameObject pauseEffectUI;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.timeScale == 1f)
            {
                if (pauseEffectUI != null)
                {
                    pauseEffectUI.SetActive(true);
                }   
                Time.timeScale = 0f;
            }
            else
            {
                if(pauseEffectUI!=null)
                {
                    pauseEffectUI.SetActive(false);
                }
                Time.timeScale = 1f;
            }
        }
    }
    public void Toggle() // for play or pause
    {
        ui.SetActive(!ui.activeSelf);
        if (ui.activeSelf)
        {
            if (pauseEffectUI != null)
            {
                pauseEffectUI.SetActive(true);
            }           
            Time.timeScale = 0f;
        }
        else
        {
            if(pauseEffectUI != null)
            {
                pauseEffectUI.SetActive(false);
            }
            Time.timeScale = 1f;
        }
    }

    public void Play()
    {
        if (pauseEffectUI != null)
        {
            pauseEffectUI.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        if (pauseEffectUI != null)
        {
            pauseEffectUI.SetActive(true);
        }
        Time.timeScale = 0f;
    }


    public void Retry()
    {
        Toggle();
        if (PlayerStats.instance == null)
        {
            PlayerStats.instance = FindObjectOfType<PlayerStats>();
        }
        PlayerStats.instance.Initialisation();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name, false);
    }
    public void Menu()
    {
        Toggle();
        if (PlayerStats.instance == null)
        {
            PlayerStats.instance = FindObjectOfType<PlayerStats>();
        }
        PlayerStats.instance.Initialisation();
        sceneFader.FadeTo(0, false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
