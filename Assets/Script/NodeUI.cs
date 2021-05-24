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
    public Button rangeUpgradeButton;
    public Button rateUpgradeButton;
    [Header("Colour")]
    public Color normalColour;
    public Color maxColour;
    [Header("Bar")]
    public Image damageBar;
    public Image rangeBar;
    public Image rateBar;
    [Header("Text")]
    public Text damageUpgradeCost;
    public Text rangeUpgradeCost;
    public Text rateUpgradeCost;
    public Text sellCost;
    public Text damageAmount;
    public Text rangeAmount;
    public Text rateAmount;
    public Text timeOfUpgradeDamage;
    public Text timeOfUpgradeRange;
    public Text timeOfUpgradeRate;
    public Text damageBuffAmount;
    public Text rangeBuffAmount;
    public Text rateBuffAmount;

    BuildManager buildManager;

    public static Node storedNode;
    private void Start()
    {
        damageBar.fillAmount = 0;
        rangeBar.fillAmount = 0;
        rateBar.fillAmount = 0;
        buildManager = BuildManager.instance;
    }
    //public Button upgradeButton;
    public void SetTarget(Node _target)
    {
        target = _target;
        storedNode = target;

        IsItMaxedLevel();
        BarFill();

        damageAmount.text = target.GetTowerObjectComponent().damage.ToString();
        rangeAmount.text = target.GetTowerObjectComponent().range.ToString();
        rateAmount.text = target.GetTowerObjectComponent().rate.ToString();

        BuffAmountToText();


        sellCost.text = $"Sell\n${target.towerTemplate.GetSellAmount()}";

        RangeArea.instance.Show(target);
        ui.SetActive(true);
        
    }

    public void Hide()
    {
        if(RangeArea.instance !=null)
        {
            RangeArea.instance.Hide();
        }
        ui.SetActive(false);
    }

    public void UpgradeDmg()
    {
        storedNode.UpgradeTowerDmg();
        buildManager.DeselectNode(false);
        buildManager.SelectNode(storedNode,false);
    }

    public void UpgradeRange()
    {
        storedNode.UpgradeTowerRange();
        buildManager.DeselectNode(false);
        buildManager.SelectNode(storedNode,false);
    }

    public void UpgradeRate()
    {
        storedNode.UpgradeTowerRate();
        buildManager.DeselectNode(false);
        buildManager.SelectNode(storedNode,false);
    }

    public void Sell()
    {
        target.SellTower();
        BuildManager.instance.DeselectNode(true);
    }

    void IsItMaxedLevel()
    {
        if (target.isDmgMaxed) // check is it level max, if is interactable the button
        {
            timeOfUpgradeDamage.text = "Max!";
            timeOfUpgradeDamage.color = Color.white;
            damageUpgradeCost.text = " - ";
            damageUpgradeButton.GetComponent<Image>().color = maxColour;
            damageUpgradeButton.interactable = false;
        }
        else
        {
            timeOfUpgradeDamage.color = Color.black;
            timeOfUpgradeDamage.text = $"{target.damageUpgradeTime} / { target.GetTowerComponent().maxDmgUpgradeTime}";
            damageUpgradeCost.text = $"${target.towerTemplate.damageUpgradeCost}";
            damageUpgradeButton.GetComponent<Image>().color = normalColour;
            damageUpgradeButton.interactable = true;
        }

        if (target.isRangeMaxed)
        {
            timeOfUpgradeRange.text = "Max!";
            timeOfUpgradeRange.color = Color.white;
            rangeUpgradeCost.text = " - ";
            rangeUpgradeButton.GetComponent<Image>().color = maxColour;
            rangeUpgradeButton.interactable = false;
        }
        else
        {
            timeOfUpgradeRange.color = Color.black;
            timeOfUpgradeRange.text = $"{target.rangeUpgradeTime} / { target.GetTowerComponent().maxRangeUpgradeTime}";
            rangeUpgradeCost.text = $"${ target.towerTemplate.rangeUpgradeCost}";
            rangeUpgradeButton.GetComponent<Image>().color = normalColour;
            rangeUpgradeButton.interactable = true;
        }

        if (target.isRateMaxed)
        {
            timeOfUpgradeRate.text = "Max!";
            timeOfUpgradeRate.color = Color.white;
            rateUpgradeCost.text = " - ";
            rateUpgradeButton.GetComponent<Image>().color = maxColour;
            rateUpgradeButton.interactable = false;
        }
        else
        {
            timeOfUpgradeRate.color = Color.black;
            timeOfUpgradeRate.text = $"{target.rateUpgradeTime} / { target.GetTowerComponent().maxRateUpgradeTime}";
            rateUpgradeCost.text = $"${ target.towerTemplate.rateUpgradeCost}";
            rateUpgradeButton.GetComponent<Image>().color = normalColour;
            rateUpgradeButton.interactable = true;
        }
    }

    void BarFill()
    {
        float currentDamageTime = target.damageUpgradeTime;
        float currentDamageMaxTime = target.GetTowerComponent().maxDmgUpgradeTime;

        damageBar.fillAmount = currentDamageTime / currentDamageMaxTime;

        float currentRangeTime = target.rangeUpgradeTime;
        float currentRangeMaxTime = target.GetTowerComponent().maxRangeUpgradeTime;

        rangeBar.fillAmount = currentRangeTime / currentRangeMaxTime;

        float currentRateTime = target.rateUpgradeTime;
        float currentRateMaxTime = target.GetTowerComponent().maxRateUpgradeTime;

        rateBar.fillAmount = currentRateTime / currentRateMaxTime;
    }

    void BuffAmountToText()
    {
        if (PlayerStats.buildingDmgBuff > 1)
            damageBuffAmount.color = Color.green;
        else
            damageBuffAmount.color = Color.white;  
        if (PlayerStats.buildingRangeBuff > 1)
            rangeBuffAmount.color = Color.green;
        else
            rangeBuffAmount.color = Color.white;
        if (PlayerStats.buildingRateBuff > 1)
            rateBuffAmount.color = Color.green;
        else
            rateBuffAmount.color = Color.white;
        
        damageBuffAmount.text = $"×{PlayerStats.buildingDmgBuff}%";
        rangeBuffAmount.text = $"×{PlayerStats.buildingRangeBuff}%";
        rateBuffAmount.text = $"×{PlayerStats.buildingRateBuff}%";
    }
}
