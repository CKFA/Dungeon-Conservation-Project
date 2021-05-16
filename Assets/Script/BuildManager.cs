using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    public GameObject CannonPrefab;
    public GameObject RocketLauncherPrefab;

    public GameObject buildEffect;

    private TowerTemplate TowerToBuild;

    public bool CanBuild { get { return TowerToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >=TowerToBuild.cost; } }

    public void BuildTowerOn(Node node)
    {
        if (PlayerStats.money< TowerToBuild.cost)
        {
            Debug.Log("Not Enough Money");
            return;
        }

        PlayerStats.money -= TowerToBuild.cost;
        GameObject tower = (GameObject)Instantiate(TowerToBuild.Prefabs, node.GetBuildPosition(), Quaternion.identity); //build tower
        node.tower = tower;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }
    public void SelectTowerToBuild(TowerTemplate tower)
    {
        TowerToBuild = tower;
    }





}
