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
    public Button sellButton;
    [Header("Text")]
    public Text upgradeCost;
    public Text sellCost;
    public Text content;
    public Text currentBuffText;
    [HideInInspector]
    public CityBuildManager cityBuildManager;
    public static CityNode storedCityNode;
    private float dmgBuff = 0f;
    private float rangeBuff = 0f;
    private float rateBuff = 0f;
    private float moneyBuff = 0f;

    //public Button upgradeButton;
    public void SetTarget(CityNode _target)
    {
        target = _target;
        storedCityNode = target;

        if (target.isMaxed)
        {
            upgradeCost.text = "Upgrade\nlv.Max";
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeCost.text = $"Upgrade\n${target.buildingTemplate.upgradeCost}";
            upgradeButton.interactable = true;
        }

        content.text = target.buildingTemplate.content;

        dmgBuff = storedCityNode.GetBuildingObjectComponent().dmgBuff + (storedCityNode.GetBuildingComponent().upgradeDmg * target.totalUpgradeTime);
        rangeBuff = storedCityNode.GetBuildingObjectComponent().rangeBuff + (storedCityNode.GetBuildingComponent().upgradeRange * target.totalUpgradeTime);
        rateBuff = storedCityNode.GetBuildingObjectComponent().rateBuff + (storedCityNode.GetBuildingComponent().upgradeRate * target.totalUpgradeTime);
        moneyBuff = storedCityNode.GetBuildingObjectComponent().moneyBuff + (storedCityNode.GetBuildingComponent().upgradeMoney * target.totalUpgradeTime);

        currentBuffText.text = $"Buff:\nDamage: +{dmgBuff}% | Range: +{rangeBuff}% | Rate: +{rateBuff}% | Money: +{moneyBuff}% ";
        sellCost.text = $"Destroy\n${target.buildingTemplate.GetSellAmount()}";
        ui.SetActive(true);
        StartCoroutine(DelayButtonInteractble());
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        storedCityNode.UpgradeBuilding();
        
        if(cityBuildManager == null)
        {
            cityBuildManager = FindObjectOfType<GameManager>().cityBuildManager;
            Debug.Log(cityBuildManager.name);
        }
        cityBuildManager.DeselectNode(false);
        cityBuildManager.SelectNode(storedCityNode,false);
        
    }

    public void Destroy()
    {
        storedCityNode.DestroyBuilding();
        if (cityBuildManager == null)
        {
            cityBuildManager = FindObjectOfType<GameManager>().cityBuildManager;
        }
        cityBuildManager.DeselectNode(true);
        ui.SetActive(false);
    }

    IEnumerator DelayButtonInteractble()
    {
        if(!target.isMaxed)
        {
            upgradeButton.interactable = false;
        }
        sellButton.interactable = false;
        yield return new WaitForSeconds(.2f);
        if (!target.isMaxed)
        {
            upgradeButton.interactable = true;
        }
        sellButton.interactable = true;
        yield break;
    }
}
