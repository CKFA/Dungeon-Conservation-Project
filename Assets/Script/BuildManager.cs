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

    private GameObject TowerToBuild;
    public GameObject GetTowerToBuild()
    {
        return TowerToBuild;
    }

    public void SetTowerToBuild(GameObject Tower)
    {
        TowerToBuild = Tower;
    }





}
