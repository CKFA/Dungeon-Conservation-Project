using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;
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
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
    public void Toggle() // for play or pause
    {
        ui.SetActive(!ui.activeSelf);
        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Play()
    {
        Time.timeScale = 1f;
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name,false);
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName,false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
