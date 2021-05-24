using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    [Header("General")]

    [Range(1f, 3f)]
    public float dmgBuff = 0f;
    [Range(0f, 1f)]
    public float upgradeDmg = 0f;
    [Range(1f, 3f)]
    public float rangeBuff = 0f;
    [Range(0f, 1f)]
    public float upgradeRange = 0f;
    [Range(1f, 3f)]
    public float rateBuff = 0f;
    [Range(0f, 1f)]
    public float upgradeRate = 0f;
    [Range(1f, 3f)]
    public float moneyBuff = 0f;
    [Range(0f, 1f)]
    public float upgradeMoney = 0f;
    [Header("Upgrade")]

    [Range(0,10)]
    public int maxUpgradeTime = 3;
    [Header("UpgradeColour")]


    public Color firstGradedColour;
    public Color secondGradedColour;
    public Color thirdGradedColour;

    [Range(0, 10)]
    public int firstGradedTime = 1;
    [Range(0, 10)]
    public int secondGradedTime = 2;
    [Range(0, 10)]
    public int thirdGradedTime = 3;

    private void Start()
    {
        firstGradedTime = 0;
        secondGradedTime = 0;
        thirdGradedTime = 0;


        firstGradedTime = maxUpgradeTime / 3;
        secondGradedTime = firstGradedTime * 2;
        thirdGradedTime = maxUpgradeTime;
    }

    public void Build()
    {
        PlayerStats.buildingDmgBuff += dmgBuff;
        PlayerStats.buildingRangeBuff += rangeBuff;
        PlayerStats.buildingRateBuff += rateBuff;
        PlayerStats.buildingMoneyBuff += moneyBuff;
    }
    public void Upgrade()
    {
        PlayerStats.buildingDmgBuff += upgradeDmg;
        PlayerStats.buildingRangeBuff += upgradeRange;
        PlayerStats.buildingRateBuff += upgradeRate;
        PlayerStats.buildingMoneyBuff += upgradeMoney;
    }

    public void Downgrade(int upgradeTime)
    {
        if(upgradeDmg!= 0) // if value not equal 0, the building have own buff, thus, need to decrease back
        {
            PlayerStats.buildingDmgBuff -= upgradeDmg * upgradeTime;
        }
        if(upgradeRange != 0)
        {
            PlayerStats.buildingRangeBuff -= upgradeRange * upgradeTime;
        }
        if(upgradeRate != 0)
        {
            PlayerStats.buildingRateBuff -= upgradeRate * upgradeTime;
        }
        if(upgradeMoney != 0)
        {
            PlayerStats.buildingMoneyBuff -= upgradeMoney * upgradeTime;
        }
        if(dmgBuff != 0)
        {
            PlayerStats.buildingDmgBuff -= dmgBuff;
        }
        if(rangeBuff != 0)
        {
            PlayerStats.buildingRangeBuff -= rangeBuff;
        }
        if(rateBuff != 0)
        {
            PlayerStats.buildingRateBuff -= rateBuff;
        }
        if(moneyBuff != 0)
        {
            PlayerStats.buildingMoneyBuff -= moneyBuff;
        }
    }
}
