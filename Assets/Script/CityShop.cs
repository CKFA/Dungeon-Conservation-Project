using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityShop : MonoBehaviour
{
    public BuildingTemplate guildTemplate;
    public BuildingTemplate apartmentTemplate;
    public BuildingTemplate bankTemplate;
    public BuildingTemplate schoolTemplate;
    public Text guildPriceText;
    public Text apartmentPriceText;
    public GameObject noEnoughMoney;

    CityBuildManager cityBuildManager;
    private void Start()
    {
        cityBuildManager = CityBuildManager.instance;
    }

    void Update()
    {
        guildPriceText.text = guildTemplate.cost.ToString();
        apartmentPriceText.text = apartmentTemplate.cost.ToString();
    }
    public void SelectGuild()
    {
        cityBuildManager.SelectBuildingToBuild(guildTemplate);
    }

    public void SelectApartment()
    {
        cityBuildManager.SelectBuildingToBuild(apartmentTemplate);
    }

    public void SelectBank()
    {
        cityBuildManager.SelectBuildingToBuild(bankTemplate);
    }

    public void SelectSchool()
    {
        cityBuildManager.SelectBuildingToBuild(schoolTemplate);
    }
}
