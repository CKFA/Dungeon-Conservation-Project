using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CityNodeData
{
    public int id;
    public GameObject buildingGameobject;
    public BuildingTemplate buildingTemplate;
    public int totalUpgradeTime;
    public bool reachedFirstGrade;
    public bool reachedSecondGrade;
    public bool reachedThirdGrade;
    public bool isMaxLevel;
}
