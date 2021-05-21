using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TowerTemplate cannonTemplate;
    public TowerTemplate launcherTemplate;
    public TowerTemplate laserTemplate;
    public Text cannonPriceText;
    public Text launcherPriceText;
    public Text laserPriceText;
    public GameObject noEnoughMoney;

    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    void Update()
    {
        cannonPriceText.text = cannonTemplate.cost.ToString();
        launcherPriceText.text = launcherTemplate.cost.ToString();
        laserPriceText.text = laserTemplate.cost.ToString();
    }

    public void SelectCannon()
    {
        Debug.Log("Selected Cannon");
        Debug.Log(cannonTemplate.Prefabs.name);
        buildManager.SelectTowerToBuild(cannonTemplate);
        Debug.Log(FindObjectOfType<BuildManager>().name);
    }
    public void SelectLauncher()
    {
        Debug.Log("RocketLauncher Selected");
        buildManager.SelectTowerToBuild(launcherTemplate);
    }

    public void SelectLaserGun()
    {
        Debug.Log("LaserGun Selected");
        buildManager.SelectTowerToBuild(laserTemplate);
    }
}
