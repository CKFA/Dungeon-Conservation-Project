using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    [Header("General")]

    public int dmgBuff = 0;
    public int upgradeDmg = 0;
    public int rangeBuff = 0;
    public int upgradeRange = 0;
    public int rateBuff = 0;
    public int upgradeRate = 0;
    public int moneyBuff = 0;
    public int upgradeMoney = 0;
    [Header("Upgrade")]

    [Range(0,10)]
    public int maxUpgradeTime = 3;
    [Header("UpgradeColour")]


    public Color firstGradedColour;
    public Color secondGradedColour;
    public Color thirdGradedColour;

    [HideInInspector]
    public int firstGradedTime = 1;
    [HideInInspector]
    public int secondGradedTime = 2;
    [HideInInspector]
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
        PlayerStats.buildingDmgBuff += (dmgBuff/100f);
        PlayerStats.buildingRangeBuff += (rangeBuff/100f);
        PlayerStats.buildingRateBuff += (rateBuff/100f);
        PlayerStats.buildingMoneyBuff += (moneyBuff/100f);
    }
    public void Upgrade()
    {
        PlayerStats.buildingDmgBuff += (upgradeDmg/100f);
        PlayerStats.buildingRangeBuff += (upgradeRange/100f);
        PlayerStats.buildingRateBuff += (upgradeRate/100f);
        PlayerStats.buildingMoneyBuff += (upgradeMoney/100f);
    }

    public void Downgrade(int upgradeTime)
    {
        if(upgradeDmg!= 0) // if value not equal 0, the building have own buff, thus, need to decrease back
        {
            PlayerStats.buildingDmgBuff -= (upgradeDmg) * upgradeTime / 100f;
        }
        if(upgradeRange != 0)
        {
            PlayerStats.buildingRangeBuff -= (upgradeRange) * upgradeTime / 100f;
        }
        if(upgradeRate != 0)
        {
            PlayerStats.buildingRateBuff -= (upgradeRate) * upgradeTime / 100f;
        }
        if(upgradeMoney != 0)
        {
            PlayerStats.buildingMoneyBuff -= (upgradeMoney) * upgradeTime / 100f;
        }
        if(dmgBuff != 0)
        {
            PlayerStats.buildingDmgBuff -= (dmgBuff / 100f);
        }
        if(rangeBuff != 0)
        {
            PlayerStats.buildingRangeBuff -= (rangeBuff/100f);
        }
        if(rateBuff != 0)
        {
            PlayerStats.buildingRateBuff -= (rateBuff/100f);
        }
        if(moneyBuff != 0)
        {
            PlayerStats.buildingMoneyBuff -= (moneyBuff/100f);
        }
    }
}
