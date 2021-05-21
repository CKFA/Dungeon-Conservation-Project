using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;
    public string defenseSceneName;
    public string townSceneName;
    public bool isThisTownStage;
    public GameObject gameOverUI;
    public BuildManager buildManager;
    public CityBuildManager cityBuildManager;
    public SceneFader sceneFader;
    public GameObject nodes;
    private int nextSceneToLoad;
    private int prevSceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;

        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        prevSceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;


        if (PlayerStats.nodesIsSpawned)
        {
            Destroy(nodes);
            Debug.Log("Destroy the nodes and keep the original nodes");
            
            if (PlayerStats.savedNodes != null) // if savenodes exists
            {
                if (isThisTownStage) // if this is town stage, disable it
                {
                    PlayerStats.savedNodes.SetActive(false);
                    Debug.Log("Set SavedNodes to False");
                    return;
                }
                else
                {

                    PlayerStats.savedNodes.SetActive(true);
                    Debug.Log("Set SavedNodes to True");
                    return;
                }
                
            }

            if (nodes == null)
            {
                nodes = PlayerStats.savedNodes;
            }
        }
        PlayerStats.nodesIsSpawned = true;

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
            ToPrevScene(false);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            ToNextScene(false);
            
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

    void ToNextScene(bool sync)
    {
        sceneFader.FadeTo(nextSceneToLoad,sync);
    }

    void ToPrevScene(bool sync)
    {
        sceneFader.FadeTo(prevSceneToLoad,sync);
    }
}
