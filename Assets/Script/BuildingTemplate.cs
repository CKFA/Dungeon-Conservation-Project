using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingTemplate
{
    public GameObject Prefabs;
    public int cost;

    public int upgradeCost = 500;
    [TextArea(5, 10)]
    public string content;
    public int GetSellAmount()
    {
        return cost / 2;
    }
}
