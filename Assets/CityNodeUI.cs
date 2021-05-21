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

    //public Button upgradeButton;
    public void SetTarget(CityNode _target)
    {
        target = _target;
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
        CityBuildManager.instance.DeselectNode();
    }

    public void Destroy()
    {
        target.DestroyBuilding();
        CityBuildManager.instance.DeselectNode();
    }

}
