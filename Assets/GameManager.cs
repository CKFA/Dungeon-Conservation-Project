using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;
    public string defenseSceneName;
    public string townSceneName;
    public GameObject gameOverUI;
    public BuildManager buildManager;
    public CityBuildManager cityBuildManager;
    public SceneFader sceneFader;

    private int nextSceneToLoad;
    private int prevSceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;

        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        prevSceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
            return;
        if (PlayerStats.hp <= 0)
        {
            EndGame();
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            EndGame();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            ToPrevScene();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            ToNextScene();
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (buildManager != null)
            {
                buildManager.DeselectNode();
                buildManager.DeselectTowerToBuild();
            }
            else if(cityBuildManager != null)
            {
                cityBuildManager.DeselectNode();
                cityBuildManager.DeselectBuildingToBuild();
            }
        }
    }

    void EndGame() // gameover
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }

    void ToNextScene()
    {
        sceneFader.FadeTo(nextSceneToLoad);
    }

    void ToPrevScene()
    {
        sceneFader.FadeTo(prevSceneToLoad);
    }
}
