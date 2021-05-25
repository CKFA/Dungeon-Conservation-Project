using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public static int money;
    public int startMoney;

    public static int hp;
    public int startHp = 20;

    public static int waves;
    public static float buildingDmgBuff;
    public static float buildingRangeBuff;
    public static float buildingRateBuff;
    public static float buildingMoneyBuff;

    public static int kills;

    [Header("Nodes")]
    public GameObject nodes;

    public static GameObject savedNodes;
    public static GameObject savedCityNodes;

    public static bool nodesIsSpawned = false; // for first spawn
    public static bool IsCityNodeSpawned = false;
    public static NodeData[] nodesData;
    public static CityNodeData[] cityNodesData;
    public int nodeDataLength;
    public int cityNodeDataLength;
    

    public static bool isInitialised = false;
    private void Awake()
    {
        if (nodesData == null)
        {
            NodeInitialisation();
        }
        
        if (!isInitialised)
        {

            money = startMoney;
            hp = startHp;
            waves = 0;
            buildingDmgBuff = 1;
            buildingRangeBuff = 1;
            buildingRateBuff = 1;
            buildingMoneyBuff = 1;

            isInitialised = true;
        }

    }
    public void Initialisation() // for game over
    {
        Debug.Log("Start to initialise");
        isInitialised = false;
        nodesIsSpawned = false;
        money = startMoney;
        hp = startHp;
        waves = 0;
        buildingDmgBuff = 1;
        buildingRangeBuff = 1;
        buildingRateBuff = 1;
        buildingMoneyBuff = 1;

        nodesData = null;

        if(savedNodes!=null)
        {
            Debug.Log("Destroys: " + savedNodes.name);
            Destroy(savedNodes);
        }
            
        if(savedCityNodes != null)
        {
            Debug.Log("Destroys: " + savedCityNodes.name);
            Destroy(savedCityNodes);
        }
            
        savedNodes = null;
        savedCityNodes = null;
    }

    public void NodeInitialisation ()
    {
        nodesData = new NodeData[nodes.transform.childCount]; // array start at 0 ,but childcount don't count 0
        for (int i = 0; i < nodesData.Length; i++)
        {
            PlayerStats.nodesData[i] = new NodeData();
            PlayerStats.nodesData[i].id = new int(); // use GetNameToInt()
            //PlayerStats.nodeData[i].towerGameobject = new GameObject();
            //PlayerStats.nodeData[i].towerTemplate = new TowerTemplate();
            PlayerStats.nodesData[i].totalUpgradeTime = new int();
            PlayerStats.nodesData[i].damageUpgradeTime = new int();
            PlayerStats.nodesData[i].rangeUpgradeTime = new int();
            PlayerStats.nodesData[i].rateUpgradeTime = new int();
            PlayerStats.nodesData[i].reachedFirstGrade = new bool();
            PlayerStats.nodesData[i].reachedSecondGrade = new bool();
            PlayerStats.nodesData[i].reachedThirdGrade = new bool();
            PlayerStats.nodesData[i].isMaxLevel = new bool();

        }
        nodeDataLength = nodesData.Length;
        savedNodes = nodes;
        DontDestroyOnLoad(savedNodes);
    }

}
