using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
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
    public static string nodesName;
    public static TowerTemplate buildedTower;
    public static bool nodeIsUpgraded;
    public static NodeData[] nodeData;

    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
        hp = startHp;
        waves = 0;
        buildingDmgBuff = 0;
        buildingRangeBuff = 0;
        buildingRateBuff = 0;
        if (nodes != null)
        {
            nodeData = new NodeData[nodes.GetComponent<Node>().transform.childCount];
            savedNodes = nodes;
            DontDestroyOnLoad(savedNodes);
        }
        else
        {
            Debug.Log(savedNodes.name + " was saved.");
            return;
        }
        if (nodesName == null) return;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
