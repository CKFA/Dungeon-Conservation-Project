using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerTemplate towerTemplate;
    public TowerTemplate launcherTemplate;
    public TowerTemplate laserTemplate;

    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectCannon()
    {
        Debug.Log("CannonPurchased");
        buildManager.SelectTowerToBuild(towerTemplate);
    }
    public void SelectLauncher()
    {
        Debug.Log("RocketLauncherPurchased");
        buildManager.SelectTowerToBuild(launcherTemplate);
    }

    public void SelectLaserGun()
    {
        Debug.Log("LaserGunPurchased");
        buildManager.SelectTowerToBuild(laserTemplate);
    }
}
