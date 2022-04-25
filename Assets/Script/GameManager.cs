using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;
    public bool isThisTownStage;
    public bool isThisTitlePage;
    public GameObject gameOverUI;
    public GameObject warningUI;
    public GameObject settingsUI;
    public GameObject creditUI;
    public GameObject howToPlayUI;
    public BuildManager buildManager;
    public CityBuildManager cityBuildManager;
    public WaveSpawner waveSpawner;
    public Button startNextWaveButton;
    public SceneFader sceneFader;
    public GameObject nodes; // use to destroy
    private GameObject cityNodes; // use to destroy

    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;

        if(isThisTitlePage)
        {
            return;
        }
        TooltipSystem.Hide();

        if(startNextWaveButton != null && !startNextWaveButton.interactable)
        {
            startNextWaveButton.interactable = true;    
        }

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
            //Debug.Log("Destroy the nodes and keep the original nodes");
        }

        if (PlayerStats.savedNodes != null) // if savenodes exists
        {
            PlayerStats.nodesIsSpawned = true;
            if (isThisTownStage) // if this is town stage, disable it
            {
                PlayerStats.savedNodes.SetActive(false);
               // Debug.Log("Set SavedNodes to False");
            }
            else
            {
                PlayerStats.savedNodes.SetActive(true);
                //Debug.Log("Set SavedNodes to True");
            }
                
        }

        if (PlayerStats.savedCityNodes != null)
        {
            if (isThisTownStage)
            {
                PlayerStats.savedCityNodes.SetActive(true);
                //Debug.Log("Set savedCityNodes to True");
            }
            else
            {
                PlayerStats.savedCityNodes.SetActive(false);
                //Debug.Log("Set savedCityNodes to False");
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
        if (PlayerStats.hp <= 0 && !isThisTitlePage && !isThisTownStage)
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Backspace) && !isThisTitlePage && !isThisTownStage)
        {
            EndGame();
        }

        //if (PlayerStats.waves % 20 == 0 && PlayerStats.waves != 0)
        //{
        //    if(waveSpawner!=null)
        //    {
        //        if (waveSpawner.isSpawning)
        //        {
        //            ShowWarningNotice("Almost daytime...", false); // call warning UI
        //            waveSpawner.isSpawning = false;
        //        }

        //    }
        //    if (startNextWaveButton != null)
        //    {
                
        //        startNextWaveButton.interactable = false;
        //    }
        //    if (WaveSpawner.EnemiesAlive <= 0 && !isThisTownStage)
        //    {
        //        ToTownBuilderScene();
        //    }

        //}

        //if (Input.GetKeyDown(KeyCode.Q) && isThisTownStage)
        //{
        //    ToTowerDefenseScene();
        //}
        //else if(Input.GetKeyDown(KeyCode.Q))
        //{
        //    ToTitleScreen();
        //}


        //if (Input.GetKeyDown(KeyCode.E) && !isThisTitlePage && !isThisTownStage)
        //{
        //    ToTownBuilderScene();

        //}

        if (Input.GetKeyDown(KeyCode.N))
        {
            waveSpawner.StartNextWave();
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

    public void ShowWarningNotice(string content,bool isWarning)
    { 
        warningUI.GetComponent<WarningUI>().warningContent = content;
        warningUI.GetComponent<WarningUI>().isWarning = isWarning;
        warningUI.SetActive(true);
    }

    public void HideWarningNotice()
    {
        warningUI.SetActive(false);
    }

    public void ToTowerDefenseScene()

    {
        if(!isThisTitlePage)
        {
            AudioManager.instance.SwitchToPlayTowerBGM();
            CityBuildManager.instance.DeselectNode(true);
            PlayerStats.waves++;
        }
        
        sceneFader.FadeTo(1, false);
    }

    public void ToTownBuilderScene()
    {
        AudioManager.instance.SwitchToPlayTownBGM();
        BuildManager.instance.DeselectNode(true);
        sceneFader.FadeTo(2, false);
    }

    public void ToTitleScreen()
    {
        if (PlayerStats.instance == null)
        {
            PlayerStats.instance = FindObjectOfType<PlayerStats>();
        }
        PlayerStats.instance.Initialisation();
        sceneFader.FadeTo(0, false);
    }

    public void CallSettings()
    {
        settingsUI.SetActive(true);
    }

    public void HideSettings()
    {
        settingsUI.SetActive(false);
    }

    public void OpenCredit()
    {
        creditUI.SetActive(true);
    }
    public void CloseCredit()
    {
        creditUI.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        howToPlayUI.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        howToPlayUI.SetActive(false);
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
