using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class NodeData
{
    public int id;
    public GameObject towerGameobject;
    public TowerTemplate towerTemplate;
    public int totalUpgradeTime;
    public int damageUpgradeTime;
    public int rangeUpgradeTime;
    public int rateUpgradeTime;
    public bool reachedFirstGrade;
    public bool reachedSecondGrade;
    public bool reachedThirdGrade;
    public bool isMaxLevel;

}
