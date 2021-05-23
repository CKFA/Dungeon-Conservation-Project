using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityNodeUI : MonoBehaviour
{
    [Header("General")]
    public GameObject ui;
    private CityNode target;
    [Header("Button")]
    public Button upgradeButton;
    [Header("Text")]
    public Text upgradeCost;
    public Text sellCost;
    CityBuildManager cityBuildManager;
    public static CityNode storedCityNode;
    private void Awake()
    {
        cityBuildManager = CityBuildManager.instance;
    }
    //public Button upgradeButton;
    public void SetTarget(CityNode _target)
    {
        target = _target;
        storedCityNode = target;

        if (!target.isMaxed)
        {
            upgradeCost.text = $"Upgrade\n${_target.buildingTemplate.upgradeCost}";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "Upgrade\nlv.Max";
            upgradeButton.interactable = false;
        }
        
        sellCost.text = $"Destroy\n${target.buildingTemplate.GetSellAmount()}";
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeBuilding();
        cityBuildManager.DeselectNode(false);
        cityBuildManager.SelectNode(storedCityNode,false);
        
    }

    public void Destroy()
    {
        target.DestroyBuilding();
        cityBuildManager.DeselectNode(true);
    }

}
