using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerTemplate
{
    public GameObject Prefabs;
    public int cost;

    public int damageUpgradeCost = 500;
    public int rangeUpgradeCost = 500;
    public int rateUpgradeCost = 500;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
