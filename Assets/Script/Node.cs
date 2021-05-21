using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [HideInInspector]
    public int id;
    public Color hoverColour;
    public Color warningColour;
    public Vector3 positionOffset;
    [HideInInspector]
    public GameObject towerObject; // the distinct tower object built in this node
    [HideInInspector]
    public TowerTemplate towerTemplate;  // a template of tower

    private Renderer rend;
    private Color startColour;
    private Color initialColour;

    [Header("Upgrade")]

    private int firstGradeTime = 0;
    private int secondGradeTime = 0;
    private int thirdGradeTime = 0;

    public bool isFirstGraded = false;
    public bool isSecondGraded = false;
    public bool isThirdGraded = false;

    public int totalUpgradeTime = 0;
    public int damageUpgradeTime = 0;
    public int rangeUpgradeTime = 0;
    public int rateUpgradeTime = 0;

    [HideInInspector]
    public bool isMaxed = false;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        id = transform.GetSiblingIndex();
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        initialColour = startColour;
        buildManager = BuildManager.instance;


    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }


    private void OnMouseEnter()
    {
        rend.material.color = hoverColour;
        if (EventSystem.current.IsPointerOverGameObject()) // if hovering the UI
        {
            return;
        }


        if (!buildManager.CanBuild) // can't build
        {
            return;
        }
            

        if (towerObject != null)
        {
            return;
        }
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColour;
        }
        else
        {
            rend.material.color = warningColour;
            BuildManager.instance.NotEnoughMoney();
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColour;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // if hovering the UI
        {
            Debug.Log(this.name + ": 1");
            return;
        }

        if (towerObject != null) // if this node have tower be built
        {
            buildManager.SelectNode(this);
            Debug.Log(this.name + ": 2");
            return;
        }

        if (!buildManager.CanBuild) // if no tower be select to build
        {
            Debug.Log(this.name + ": 3");
            return;
        }

        Debug.Log(this.name + ": 4");
        BuildTower(buildManager.GetTowerToBuild()); // build
    }

    void BuildTower(TowerTemplate template)
    {
        if (PlayerStats.money < template.cost)
        {
            BuildManager.instance.NotEnoughMoney();
            return;
        }

        PlayerStats.money -= template.cost;
        GameObject _tower = (GameObject)Instantiate(template.Prefabs, GetBuildPosition(), Quaternion.identity); //build tower
        towerObject = _tower;
        towerObject.transform.parent = gameObject.transform;

        towerTemplate = template;

        SaveTower();

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public bool UpgradeTowerDmg(bool isTradeSuccess)
    {
        if (PlayerStats.money < towerTemplate.damageUpgradeCost)
        {
            BuildManager.instance.NotEnoughMoney();
            
            isTradeSuccess = false;
            return isTradeSuccess;
        }

        DamageUpgradeAdder();
        PlayerStats.money -= towerTemplate.damageUpgradeCost;

        ColourChanger(); // with grade checker
        SaveTower();

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        towerObject.GetComponent<Tower>().bulletPrefab.GetComponent<Bullet>().startDamage += 10; // add value
        isTradeSuccess = true;
        return isTradeSuccess;
        // if max
        //isUpgraded = true;
    }

    public void UpgradeTowerRange()
    {
        if (!towerObject.GetComponent<Tower>().CheckIsMaxRange)
        {
            if (PlayerStats.money < towerTemplate.rangeUpgradeCost)
            {
                Debug.Log("Not Enough Money");
                return;
            }

            RangeUpgradeAdder();
            PlayerStats.money -= towerTemplate.rangeUpgradeCost;

            ColourChanger(); // with grade checker
            SaveTower();

            GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

            towerObject.GetComponent<Tower>().UpgradeRange();
        }
    }

    public void UpgradeTowerRate()
    {
        if(!towerObject.GetComponent<Tower>().CheckIsMaxRate)
        {
            if (PlayerStats.money < towerTemplate.rateUpgradeCost)
            {
                Debug.Log("Not Enough Money");
                return;
            }

            RateUpgradeAdder();
            PlayerStats.money -= towerTemplate.rateUpgradeCost;

            ColourChanger(); // with grade checker
            SaveTower();

            GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

            towerObject.GetComponent<Tower>().UpgradeRate();
        }

    }

    public void SellTower()
    {
        PlayerStats.money += towerTemplate.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(towerObject);
        towerTemplate = null;
    }

    public Bullet GetBullet()
    {
        return towerObject.GetComponent<Tower>().bulletPrefab.GetComponent<Bullet>();
    }

    public Tower GetTower()
    {
        return towerObject.GetComponent<Tower>();
    }

    public Tower GetTowerComponent()
    {
        return towerTemplate.Prefabs.GetComponent<Tower>();
    }
    public int GradeChecker() // for check grade
    {
        if ((totalUpgradeTime == firstGradeTime) && (!isFirstGraded))
        {
            isFirstGraded = true;
            return 1;
        }
        else if ((totalUpgradeTime == secondGradeTime) && (!isSecondGraded))
        {
            isSecondGraded = true;
            return 2;
        }
        else if ((totalUpgradeTime == thirdGradeTime) && (!isThirdGraded))
        {
            isThirdGraded = true;
            return 3;
        }
        else if (isThirdGraded)
        {
            return 3;
        }
        return 0;
    }

    public void ColourChanger()
    {
        int gradeLevel = GradeChecker();
        switch (gradeLevel)
        {
            case 1:
                startColour = GetTowerComponent().firstGradedColour.color;
                rend.material.color = startColour;
                break;
            case 2:
                startColour = GetTowerComponent().secondGradedColour.color;
                rend.material.color = startColour;
                break;
            case 3:
                startColour = GetTowerComponent().thirdGradedColour.color;
                rend.material.color = startColour;
                isMaxed = true;
                break;
            default:
                break;
        }
    }

    public bool DamageUpgradeAdder()
    {
        if (totalUpgradeTime < GetTowerComponent().bulletPrefab.GetComponent<Bullet>().maxDamage)
        {
            totalUpgradeTime++;
            Debug.Log(this.name + "'s damage upgrade time: " + totalUpgradeTime);
            return true;
        }
        else
            return false;
    }

    public bool RangeUpgradeAdder()
    {
        if (totalUpgradeTime < GetTowerComponent().maxRange)
        {
            totalUpgradeTime++;
            Debug.Log(this.name + "'s range upgrade time: " + totalUpgradeTime);
            return true;
        }
        else
            return false;
    }

    public bool RateUpgradeAdder()
    {
        if (totalUpgradeTime < GetTowerComponent().maxRate)
        {
            totalUpgradeTime++;
            Debug.Log(this.name + "'s rate upgrade time: " + totalUpgradeTime);
            return true;
        }
        else
            return false;
    }

    public void Initialise()
    {
        startColour = initialColour;
        rend.material.color = initialColour;

        isFirstGraded = false;
        isSecondGraded = false;
        isThirdGraded = false;

        isMaxed = false;

        totalUpgradeTime = 0;
        ClearTower();
    }

    public void SaveTower()
    {
        PlayerStats.nodeData[id].id = id; // use GetNameToInt()
        PlayerStats.nodeData[id].towerGameobject = towerObject;
        PlayerStats.nodeData[id].towerTemplate = towerTemplate;
        PlayerStats.nodeData[id].totalUpgradeTime = totalUpgradeTime;
        PlayerStats.nodeData[id].damageUpgradeTime = damageUpgradeTime;
        PlayerStats.nodeData[id].rangeUpgradeTime = rangeUpgradeTime;
        PlayerStats.nodeData[id].rateUpgradeTime = rateUpgradeTime;
        PlayerStats.nodeData[id].reachedFirstGrade = isFirstGraded;
        PlayerStats.nodeData[id].reachedSecondGrade = isSecondGraded;
        PlayerStats.nodeData[id].reachedThirdGrade = isThirdGraded;
    }

    public void ClearTower()
    {
        PlayerStats.nodeData[id].id = id; // use GetNameToInt()
        PlayerStats.nodeData[id].towerGameobject = null;
        PlayerStats.nodeData[id].towerTemplate = null;
        PlayerStats.nodeData[id].totalUpgradeTime = 0;
        PlayerStats.nodeData[id].damageUpgradeTime = 0;
        PlayerStats.nodeData[id].rangeUpgradeTime = 0;
        PlayerStats.nodeData[id].rateUpgradeTime = 0;
        PlayerStats.nodeData[id].reachedFirstGrade = false;
        PlayerStats.nodeData[id].reachedSecondGrade = false;
        PlayerStats.nodeData[id].reachedThirdGrade = false;
        PlayerStats.nodeData[id].isMaxLevel = false;
    }
    
    public void LoadTower()
    {

        towerObject = PlayerStats.nodeData[id].towerGameobject;
        towerTemplate = PlayerStats.nodeData[id].towerTemplate;
        totalUpgradeTime = PlayerStats.nodeData[id].totalUpgradeTime;
        damageUpgradeTime = PlayerStats.nodeData[id].damageUpgradeTime;
        rangeUpgradeTime = PlayerStats.nodeData[id].rangeUpgradeTime;
        rateUpgradeTime = PlayerStats.nodeData[id].rateUpgradeTime;
        isFirstGraded = PlayerStats.nodeData[id].reachedFirstGrade;
        isSecondGraded = PlayerStats.nodeData[id].reachedSecondGrade;
        isThirdGraded = PlayerStats.nodeData[id].reachedThirdGrade;
        
    }
}
