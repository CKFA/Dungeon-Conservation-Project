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

    public bool isFirstGraded = false;
    public bool isSecondGraded = false;
    public bool isThirdGraded = false;

    public int totalUpgradeTime;
    public int damageUpgradeTime;
    public int rangeUpgradeTime;
    public int rateUpgradeTime;

    [HideInInspector]
    public bool isDmgMaxed = false;
    [HideInInspector]
    public bool isRangeMaxed = false;
    [HideInInspector]
    public bool isRateMaxed = false;

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
            return;
        }
        //Debug.Log(this.name + ": " + isDmgMaxed);
        if (towerObject != null) // if this node have tower be built
        {
            buildManager.SelectNode(this,true);
            //Debug.Log(this.name + " is Selected");
            return;
        }

        if (!buildManager.CanBuild) // if no tower be select to build
        {
            return;
        }

        //Debug.Log(this.name + ": 4");
        BuildTower(buildManager.GetTowerToBuild()); // build
    }

    void BuildTower(TowerTemplate template)
    {
        if (PlayerStats.money < template.cost)
        {
            FindObjectOfType<GameManager>().ShowWarningNotice("Not Enough Money to Build!",true);
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

    public void UpgradeTowerDmg()
    {
        if (PlayerStats.money < towerTemplate.damageUpgradeCost)
        {
            FindObjectOfType<GameManager>().ShowWarningNotice("Failed to upgrade Damage, reason: No money",true);
            Debug.Log("Failed to upgrade Dmg, reason: No money");
        }

        if (DamageUpgradeAdder())
        {
            towerObject.GetComponent<Tower>().UpgradeDamage();
            PlayerStats.money -= towerTemplate.damageUpgradeCost;

            int gradeLevel = GradeChecker();
            ColourChanger(gradeLevel);
            SaveTower();

            GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);
        }

        // if max
        //isUpgraded = true;
    }

    public void UpgradeTowerRange()
    {
        if (PlayerStats.money < towerTemplate.rangeUpgradeCost)
        {
            FindObjectOfType<GameManager>().ShowWarningNotice("Failed to upgrade Range, reason: No money",true);
            Debug.Log("Failed to upgrade Range, reason: No money");
            return;
        }

        if (RangeUpgradeAdder())
        {
            towerObject.GetComponent<Tower>().UpgradeRange();
            PlayerStats.money -= towerTemplate.rangeUpgradeCost;

            int gradeLevel = GradeChecker();
            ColourChanger(gradeLevel); // with grade checker
            SaveTower();

            GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

        
        }
    }

    public void UpgradeTowerRate()
    {
        if (PlayerStats.money < towerTemplate.rateUpgradeCost)
        {
            FindObjectOfType<GameManager>().ShowWarningNotice("Failed to upgrade Rate, reason: No money!",true);
            Debug.Log("Failed to upgrade Rate, reason: No money!");
            return;
        }

        if (RateUpgradeAdder())
        {
            towerObject.GetComponent<Tower>().UpgradeRate();
            PlayerStats.money -= towerTemplate.rateUpgradeCost;

            int gradeLevel = GradeChecker();
            ColourChanger(gradeLevel); // with grade checker
            SaveTower();

            GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);
        }

    }

    public void SellTower()
    {
        PlayerStats.money += towerTemplate.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Initialise();
        Destroy(towerObject);
        towerTemplate = null;
    }

    public Bullet GetBullet()
    {
        return towerObject.GetComponent<Tower>().bulletPrefab.GetComponent<Bullet>();
    }

    public Tower GetTowerObjectComponent()
    {
        return towerObject.GetComponent<Tower>();
    }

    public Tower GetTowerComponent()
    {
        return towerTemplate.Prefabs.GetComponent<Tower>();
    }
    public int GradeChecker() // for check grade
    {
        if ((totalUpgradeTime == GetTowerObjectComponent().firstGradedTime) && (!isFirstGraded))
        {
            isFirstGraded = true;
            //Debug.Log(this.name + ": 1 graded!!!");
            return 1;
        }
        else if ((totalUpgradeTime == GetTowerObjectComponent().secondGradedTime) && (!isSecondGraded))
        {
            isSecondGraded = true;
            return 2;
        }
        else if ((totalUpgradeTime == GetTowerObjectComponent().thirdGradedTime) && (!isThirdGraded))
        {
            isThirdGraded = true;
            return 3;
        }
        else if (isThirdGraded)
        {
            return 3;
        }
        //Debug.Log(this.name + ": " + totalUpgradeTime + " / " + GetTowerObjectComponent().firstGradedTime);
        return 0;
    }

    public void ColourChanger(int gradeLevel)
    {
        switch (gradeLevel)
        {
            case 1:
                GetTowerObjectComponent().partToChange.material= GetTowerComponent().firstGradedColour;
                //Debug.Log(this.name + ": 1 Graded");
                break;
            case 2:
                GetTowerObjectComponent().partToChange.material = GetTowerComponent().secondGradedColour;
                //startColour = GetTowerComponent().secondGradedColour.color;
                //rend.material.color = startColour;
                //Debug.Log(this.name + ": 2 Graded");
                break;
            case 3:
                GetTowerObjectComponent().partToChange.material = GetTowerComponent().thirdGradedColour;
                //Debug.Log(this.name + ": 3 Graded");
                break;
            default:
                break;
        }
    }

    public bool DamageUpgradeAdder()
    {
        if (damageUpgradeTime < GetTowerComponent().maxDmgUpgradeTime)
        {
            totalUpgradeTime++;
            damageUpgradeTime++;
            Debug.Log(this.name + "'s damage upgrade time: " + damageUpgradeTime);

            if (damageUpgradeTime == GetTowerComponent().maxDmgUpgradeTime)
            {
                isDmgMaxed = true;
            }

            return true;
        }
        else
        {
            Debug.Log(this.name + "'s range level: " + damageUpgradeTime + " (max)");
            return false;
        }

    }

    public bool RangeUpgradeAdder()
    {
        if (rangeUpgradeTime < GetTowerComponent().maxRangeUpgradeTime) // if upgraded time less than max value from tower.
        {
            totalUpgradeTime++;
            rangeUpgradeTime++;
            Debug.Log(this.name + "'s range upgrade time: " + rangeUpgradeTime);

            if (rangeUpgradeTime == GetTowerComponent().maxRangeUpgradeTime)
            {
                isRangeMaxed = true;
            }

            return true;
        }
        else
        {
           Debug.Log(this.name + "'s range level: " + rangeUpgradeTime + " (max)");
            return false;
        }
    }

    public bool RateUpgradeAdder()
    {
        if (rateUpgradeTime < GetTowerComponent().maxRateUpgradeTime)
        {
            totalUpgradeTime++;
            rateUpgradeTime++;
            //Debug.Log(this.name + "'s rate upgrade time: " + rateUpgradeTime);

            if (rateUpgradeTime == GetTowerComponent().maxRateUpgradeTime)
            {
                //Debug.Log(this.name + "'s range level: " + rateUpgradeTime + " (max)");
                isRateMaxed = true;
            }

            return true;
        }
        else
        {
            //Debug.Log(this.name + "'s range level: " + rateUpgradeTime + " (max)");
            return false;
        }

    }

    public void Initialise()  //初期化する Reset all the data
    {
        startColour = initialColour;
        rend.material.color = initialColour;

        isFirstGraded = false;
        isSecondGraded = false;
        isThirdGraded = false;
        

        isDmgMaxed = false;
        isRangeMaxed = false;
        isRateMaxed = false;

        totalUpgradeTime = 0;
        damageUpgradeTime = 0;
        rangeUpgradeTime = 0;
        rateUpgradeTime = 0;

        ClearTower();
    }

    public void SaveTower()
    {
        PlayerStats.nodesData[id].id = id; // use GetNameToInt()
        PlayerStats.nodesData[id].towerGameobject = towerObject;
        PlayerStats.nodesData[id].towerTemplate = towerTemplate;
        PlayerStats.nodesData[id].totalUpgradeTime = totalUpgradeTime;
        PlayerStats.nodesData[id].damageUpgradeTime = damageUpgradeTime;
        PlayerStats.nodesData[id].rangeUpgradeTime = rangeUpgradeTime;
        PlayerStats.nodesData[id].rateUpgradeTime = rateUpgradeTime;
        PlayerStats.nodesData[id].reachedFirstGrade = isFirstGraded;
        PlayerStats.nodesData[id].reachedSecondGrade = isSecondGraded;
        PlayerStats.nodesData[id].reachedThirdGrade = isThirdGraded;
    }

    public void ClearTower()
    {
        PlayerStats.nodesData[id].id = id; // use GetNameToInt()
        PlayerStats.nodesData[id].towerGameobject = null;
        PlayerStats.nodesData[id].towerTemplate = null;
        PlayerStats.nodesData[id].totalUpgradeTime = 0;
        PlayerStats.nodesData[id].damageUpgradeTime = 0;
        PlayerStats.nodesData[id].rangeUpgradeTime = 0;
        PlayerStats.nodesData[id].rateUpgradeTime = 0;
        PlayerStats.nodesData[id].reachedFirstGrade = false;
        PlayerStats.nodesData[id].reachedSecondGrade = false;
        PlayerStats.nodesData[id].reachedThirdGrade = false;
        PlayerStats.nodesData[id].isMaxLevel = false;
    }

    public void LoadTower()
    {

        towerObject = PlayerStats.nodesData[id].towerGameobject;
        towerTemplate = PlayerStats.nodesData[id].towerTemplate;
        totalUpgradeTime = PlayerStats.nodesData[id].totalUpgradeTime;
        damageUpgradeTime = PlayerStats.nodesData[id].damageUpgradeTime;
        rangeUpgradeTime = PlayerStats.nodesData[id].rangeUpgradeTime;
        rateUpgradeTime = PlayerStats.nodesData[id].rateUpgradeTime;
        isFirstGraded = PlayerStats.nodesData[id].reachedFirstGrade;
        isSecondGraded = PlayerStats.nodesData[id].reachedSecondGrade;
        isThirdGraded = PlayerStats.nodesData[id].reachedThirdGrade;

    }
}
