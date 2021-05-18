using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerTemplate
{
    public GameObject Prefabs;
    public int cost;

    public GameObject upgradedPrefabs;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
