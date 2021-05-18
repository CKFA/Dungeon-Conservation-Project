using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public Text upgradeCost;
    public Text sellCost;
    public Button upgradeButton;
    public void SetTarget(Node _target)
    {
        target = _target;
        if(!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.towerTemplate.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "Max";
            upgradeButton.interactable = false;
        }

        sellCost.text = "$" + target.towerTemplate.GetSellAmount();

        ui.SetActive(true);

        
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTower();
        BuildManager.instance.DeselectNode();
    }
}
