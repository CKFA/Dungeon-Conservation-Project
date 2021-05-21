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
    public Button upgradeDamageButton;
    public Button upgradeRangeButton;
    public Button upgradeRateButton;
    [Header("Bar")]
    public Image upgradeDamageBar;
    public Image upgradeRangeBar;
    public Image upgradeRateBar;
    [Header("Text")]
    public Text upgradeDmgCost;
    public Text upgradeRangeCost;
    public Text upgradeRateCost;
    public Text sellCost;

    private void Start()
    {
        upgradeDamageBar.fillAmount = 0;
        upgradeRangeBar.fillAmount = 0;
        upgradeRateBar.fillAmount = 0;
    }
    //public Button upgradeButton;
    public void SetTarget(Node _target)
    {
        target = _target;
        //if(!target.isUpgraded)
        //{
        //    upgradeDmgCost.text = "$" + target.towerTemplate.upgradeCost;
        //    upgradeButton.interactable = true;
        //}
        //else
        //{
        //    upgradeDmgCost.text = "Max";
        //    upgradeButton.interactable = false;
        //}
        
        upgradeDmgCost.text = $"${_target.towerTemplate.damageUpgradeCost}";
        upgradeRangeCost.text = $"${ _target.towerTemplate.rangeUpgradeCost}";
        upgradeRateCost.text = $"${ _target.towerTemplate.rateUpgradeCost}";
        sellCost.text = $"Sell\n${target.towerTemplate.GetSellAmount()}";
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void UpgradeDmg()
    {
        bool isTradeSuccess = false;
        if(target.UpgradeTowerDmg(isTradeSuccess))
        {
            upgradeDamageBar.fillAmount += target.GetBullet().damage / target.GetBullet().maxDamage;
        }
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
