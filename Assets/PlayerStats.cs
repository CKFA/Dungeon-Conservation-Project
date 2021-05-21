using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public static PlayerPrefs instance;
    public static int money;
    public int startMoney;

    public static int hp;
    public int startHp = 20;

    public static int waves;
    public static float buildingDmgBuff;
    public static float buildingRangeBuff;
    public static float buildingRateBuff;

    [Header("Nodes")]
    public GameObject nodes;
    public static GameObject savedNodes;
    public static bool nodesIsSpawned = false; // for first spawn
    public static NodeData[] nodeData;
    public int nodeDataLength;

    public static bool isInitialised = false;
    private void Awake()
    {

        if (nodeData == null)
        {
            nodeData = new NodeData[nodes.transform.childCount]; // array start at 0 ,but childcount don't count 0
            for (int i = 0; i < nodeData.Length; i++)
            {
                PlayerStats.nodeData[i] = new NodeData();
                PlayerStats.nodeData[i].id = new int(); // use GetNameToInt()
                //PlayerStats.nodeData[i].towerGameobject = new GameObject();
                //PlayerStats.nodeData[i].towerTemplate = new TowerTemplate();
                PlayerStats.nodeData[i].totalUpgradeTime = new int();
                PlayerStats.nodeData[i].damageUpgradeTime = new int();
                PlayerStats.nodeData[i].rangeUpgradeTime = new int();
                PlayerStats.nodeData[i].rateUpgradeTime = new int();
                PlayerStats.nodeData[i].reachedFirstGrade = new bool();
                PlayerStats.nodeData[i].reachedSecondGrade = new bool();
                PlayerStats.nodeData[i].reachedThirdGrade = new bool();

            }
            nodeDataLength = nodeData.Length;
            savedNodes = nodes;
            DontDestroyOnLoad(savedNodes);
        }
        if (!isInitialised)
        {

            money = startMoney;
            hp = startHp;
            waves = 0;
            buildingDmgBuff = 0;
            buildingRangeBuff = 0;
            buildingRateBuff = 0;


            isInitialised = true;
        }


    }
    public void Initialisation()
    {
        isInitialised = false;
        nodesIsSpawned = false;
        money = startMoney;
        hp = startHp;
        waves = 0;
        buildingDmgBuff = 0;
        buildingRangeBuff = 0;
        buildingRateBuff = 0;

        nodes = null;
        savedNodes = null;
        nodeData = null;
    }

}
