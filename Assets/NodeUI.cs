using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [Header("General")]
    public GameObject ui;
    private Node target;
    [Header("Buttons")]
    public Button damageUpgradeButton;
    public RectTransform rangeUpgradeButton;
    public Button rateUpgradeButton;
    [Header("Bar")]
    public Image damageBar;
    public Image rangeBar;
    public Image rateBar;
    [Header("Text")]
    public Text upgradeDmgCost;
    public Text upgradeRangeCost;
    public Text upgradeRateCost;
    public Text sellCost;
    public Text dmgAmount;
    public Text rangeAmount;
    public Text rateAmount;
    public Text timeOfUpgradeDmg;
    public Text timeOfUpgradeRange;
    public Text timeOfUpgradeRate;

    private void Start()
    {
        damageBar.fillAmount = 0;
        rangeBar.fillAmount = 0;
        rateBar.fillAmount = 0;
    }
    //public Button upgradeButton;
    public void SetTarget(Node _target)
    {
        target = _target;
        //if (!target.isUpgraded)
        //{
        //    upgradeDmgCost.text = "$" + target.towerTemplate.upgradeCost;
        //    upgradeButton.interactable = true;
        //}
        //else
        //{
        //    upgradeDmgCost.text = "Max";
        //    upgradeButton.interactable = false;
        //}

        float currentDamageTime = target.damageUpgradeTime;
        float currentDamageMaxTime = target.GetTowerComponent().maxDmgUpgradeTime;

        damageBar.fillAmount = currentDamageTime / currentDamageMaxTime;

        float currentRangeTime = target.rangeUpgradeTime;
        float currentRangeMaxTime = target.GetTowerComponent().maxRangeUpgradeTime;

        rangeBar.fillAmount = currentRangeTime / currentRangeMaxTime;
        
        float currentRateTime = target.rateUpgradeTime;
        float currentRateMaxTime = target.GetTowerComponent().maxRateUpgradeTime;

        rateBar.fillAmount = currentRateTime / currentRateMaxTime;
        
        timeOfUpgradeRange.text = $"{target.rangeUpgradeTime} / { target.GetTowerComponent().maxRangeUpgradeTime}";
        timeOfUpgradeRate.text = $"{target.rateUpgradeTime} / { target.GetTowerComponent().maxRateUpgradeTime}";


        rangeAmount.text = target.GetTowerObjectComponenet().range.ToString();
        rateAmount.text = target.GetTowerObjectComponenet().rate.ToString();

        upgradeDmgCost.text = $"${target.towerTemplate.damageUpgradeCost}";
        upgradeRangeCost.text = $"${ target.towerTemplate.rangeUpgradeCost}";
        upgradeRateCost.text = $"${ target.towerTemplate.rateUpgradeCost}";
        sellCost.text = $"Sell\n${target.towerTemplate.GetSellAmount()}";

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void UpgradeDmg()
    {

        target.UpgradeTowerDmg();

        BuildManager.instance.DeselectNode();
    }

    public void UpgradeRange()
    {
        target.UpgradeTowerRange();
        BuildManager.instance.DeselectNode();
    }

    public void UpgradeRate()
    {
        target.UpgradeTowerRate();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTower();
        BuildManager.instance.DeselectNode();
    }

}
