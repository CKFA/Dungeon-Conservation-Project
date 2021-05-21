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
        Debug.Log("CannonPurchased");
        buildManager.SelectTowerToBuild(cannonTemplate);
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
