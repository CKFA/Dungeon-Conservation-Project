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
    public GameObject nodes; // use to destroy
    private GameObject cityNodes; // use to destroy
    private int nextSceneToLoad;
    private int prevSceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;

        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        prevSceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;

        TooltipSystem.Hide();

        if(isThisTownStage && PlayerStats.savedCityNodes == null)
        {
            cityNodes = FindObjectOfType<CityNode>().gameObject.transform.parent.gameObject;
            Debug.Log("CityNode: " + cityNodes.name + " Found!");
            CityNodeInitialisation();
            
        }
        else if(isThisTownStage && PlayerStats.savedCityNodes != null)
        {
            cityNodes = FindObjectOfType<CityNode>().gameObject.transform.parent.gameObject;
            Destroy(cityNodes);
        }

        if(isThisTownStage && cityBuildManager == null)
        {
            cityBuildManager = FindObjectOfType<CityBuildManager>();
        }

        if (PlayerStats.nodesIsSpawned) // if node is spawned
        {
            if(nodes!=null)
            {
                Destroy(nodes);
            }
            Debug.Log("Destroy the nodes and keep the original nodes");
        }

        if (PlayerStats.savedNodes != null) // if savenodes exists
        {
            PlayerStats.nodesIsSpawned = true;
            if (isThisTownStage) // if this is town stage, disable it
            {
                PlayerStats.savedNodes.SetActive(false);
                Debug.Log("Set SavedNodes to False");
            }
            else
            {
                PlayerStats.savedNodes.SetActive(true);
                Debug.Log("Set SavedNodes to True");
            }
                
        }

        if (PlayerStats.savedCityNodes != null)
        {
            if (isThisTownStage)
            {
                PlayerStats.savedCityNodes.SetActive(true);
                Debug.Log("Set savedCityNodes to True");
            }
            else
            {
                PlayerStats.savedCityNodes.SetActive(false);
                Debug.Log("Set savedCityNodes to False");
            }
        }
        
       

        if (nodes == null)
        {
            nodes = PlayerStats.savedNodes;
        }
        if (cityNodes == null)
        {
            cityNodes = PlayerStats.savedCityNodes;
        }
        if (buildManager == null)
        {
            return;
        }
        else if ( cityBuildManager == null)
        {
            return;
        }
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
                buildManager.DeselectNode(true);
                buildManager.DeselectTowerToBuild();
            }
            else if(cityBuildManager != null)
            {
                cityBuildManager.DeselectNode(true);
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
        BuildManager.instance.DeselectNode(true);
        sceneFader.FadeTo(nextSceneToLoad,sync);
    }

    void ToPrevScene(bool sync)
    {
        CityBuildManager.instance.DeselectNode(true);
        sceneFader.FadeTo(prevSceneToLoad,sync);
    }

    void CityNodeInitialisation()
    {
        PlayerStats.cityNodesData = new CityNodeData[cityNodes.transform.childCount];
        for (int i = 0; i < PlayerStats.cityNodesData.Length; i++)
        {
            PlayerStats.cityNodesData[i] = new CityNodeData();
            PlayerStats.cityNodesData[i].id = new int();
            PlayerStats.cityNodesData[i].totalUpgradeTime = new int();
            PlayerStats.cityNodesData[i].reachedFirstGrade = new bool();
            PlayerStats.cityNodesData[i].reachedSecondGrade = new bool();
            PlayerStats.cityNodesData[i].reachedThirdGrade = new bool();
            PlayerStats.cityNodesData[i].isMaxLevel = new bool();

        }
        //PlayerStats.instance.cityNodeDataLength = PlayerStats.cityNodesData.Length;
        PlayerStats.savedCityNodes = cityNodes;
        DontDestroyOnLoad(PlayerStats.savedCityNodes);
    }
}
