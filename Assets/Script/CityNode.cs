using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CityNode : MonoBehaviour
{
    public int id;
    [Header("General")]
    public Color hoverColour;
    public Color warningColour;
    public Vector3 positionOffset;
    [HideInInspector]
    public GameObject buildingObject;
    [HideInInspector]
    public BuildingTemplate buildingTemplate;

    [HideInInspector]
    public Renderer rend;
    private Color startColour;
    private Color initalColour;

    [Header("Upgrade")]

    public bool isFirstGraded = false;
    public bool isSecondGraded = false;
    public bool isThirdGraded = false;

    public int totalUpgradeTime = 0;
    [HideInInspector]
    public bool isMaxed = false;

    CityBuildManager cityBuildManager;

    // Start is called before the first frame update
    void Start()
    {
        id = transform.GetSiblingIndex();
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        cityBuildManager = CityBuildManager.instance;
        initalColour = startColour;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }


    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // if hovering the UI
            return;

        if (!cityBuildManager.CanBuild)
        return;

        if (buildingObject != null)
        {
            return;
        }
        if (cityBuildManager.HasMoney)
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
            return;

        if (buildingObject != null) // if the tower built
        {
            cityBuildManager.SelectNode(this,true); // select this node
            return;
        }

        if (!cityBuildManager.CanBuild) // if can't build
            return;

        BuildBuilding(cityBuildManager.GetBuildingToBuild()); // build
    }

    void BuildBuilding(BuildingTemplate template)
    {
        if (PlayerStats.money < template.cost)
        {
            FindObjectOfType<GameManager>().ShowWarningNotice("Not Enough Money!", true);
            return;
        }

        PlayerStats.money -= template.cost;
        GameObject _building = (GameObject)Instantiate(template.Prefabs, GetBuildPosition(), Quaternion.identity); //build building
        buildingObject = _building;
        buildingObject.transform.parent = gameObject.transform;

        buildingTemplate = template;
        GetBuildingObjectComponent().Build();
        SaveBuilding();

        GameObject effect = (GameObject)Instantiate(cityBuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void DestroyBuilding()
    {
        PlayerStats.money += buildingTemplate.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(cityBuildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        
        GetBuildingObjectComponent().Downgrade(totalUpgradeTime);

        Initialise();
        Destroy(buildingObject);
        buildingTemplate = null;
    }

    public void UpgradeBuilding()
    {
        if (PlayerStats.money < buildingTemplate.upgradeCost)
        {
            FindObjectOfType<GameManager>().ShowWarningNotice("Not Enough Money!", true);

            return;
        }

        if(TimesUpgradeAdder()) // upgrade time ++
        {
            buildingObject.GetComponent<Building>().Upgrade();
            PlayerStats.money -= buildingTemplate.upgradeCost;

            int gradeLevel = GradeChecker();
            ColourChanger(gradeLevel);
            //SaveBuilding();

            GameObject effect = (GameObject)Instantiate(cityBuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);
        }
        else
        {
            Debug.Log("Fail to upgrade!");
        }
    }

    public Renderer GetRenderer()
    {
        return rend;
    }

    public Building GetBuildingComponent( )
    {
        return buildingTemplate.Prefabs.GetComponent<Building>();
    }

    public Building GetBuildingObjectComponent()
    {
        return buildingObject.GetComponent<Building>();
    }

    public int GradeChecker() // for check grade
    {
        if ((totalUpgradeTime == GetBuildingObjectComponent().firstGradedTime) && (!isFirstGraded))
        {
            isFirstGraded = true;
            return 1;
        }
        else if ((totalUpgradeTime == GetBuildingObjectComponent().secondGradedTime) && (!isSecondGraded))
        {
            isSecondGraded = true;
            return 2;
        }
        else if ((totalUpgradeTime == GetBuildingObjectComponent().thirdGradedTime) && (!isThirdGraded))
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

    public void ColourChanger(int gradeLevel)
    {
        switch (gradeLevel)
        {
            case 1:
                startColour = GetBuildingObjectComponent().firstGradedColour;
                rend.material.color = startColour;
                break;
            case 2:
                startColour = GetBuildingObjectComponent().secondGradedColour;
                rend.material.color = startColour;
                break;
            case 3:
                startColour = GetBuildingObjectComponent().thirdGradedColour;
                rend.material.color = startColour;
                break;
            default:
                break;
        }
    }
    public int TimesUpgradeChecker()
    {
        int _upgradeTime = totalUpgradeTime;
        return _upgradeTime;
    }

    public bool TimesUpgradeAdder()
    {

        if (totalUpgradeTime < GetBuildingObjectComponent().maxUpgradeTime && !isMaxed)
        {
            totalUpgradeTime++;
            Debug.Log(this.name + " " + totalUpgradeTime);
            if (totalUpgradeTime == GetBuildingObjectComponent().maxUpgradeTime)
            {
                isMaxed = true;
            }
            return true;
        }
        else
            return false;
    }

    public void Initialise()
    {
        startColour = initalColour;
        rend.material.color = initalColour;

        isFirstGraded = false;
        isSecondGraded = false;
        isThirdGraded = false;

        isMaxed = false;

        totalUpgradeTime = 0;
        //ClearBuilding();
    }

    public void SaveBuilding()
    {
        PlayerStats.cityNodesData[id].id = id; // use GetNameToInt()
        PlayerStats.cityNodesData[id].buildingGameobject = buildingObject;
        PlayerStats.cityNodesData[id].buildingTemplate = buildingTemplate;
        PlayerStats.cityNodesData[id].totalUpgradeTime = totalUpgradeTime;
        PlayerStats.cityNodesData[id].reachedFirstGrade = isFirstGraded;
        PlayerStats.cityNodesData[id].reachedSecondGrade = isSecondGraded;
        PlayerStats.cityNodesData[id].reachedThirdGrade = isThirdGraded;
    }

    public void ClearBuilding()
    {
        PlayerStats.cityNodesData[id].id = id; // use GetNameToInt()
        PlayerStats.cityNodesData[id].buildingGameobject = null;
        PlayerStats.cityNodesData[id].buildingTemplate = null;
        PlayerStats.cityNodesData[id].totalUpgradeTime = 0;
        PlayerStats.cityNodesData[id].reachedFirstGrade = false;
        PlayerStats.cityNodesData[id].reachedSecondGrade = false;
        PlayerStats.cityNodesData[id].reachedThirdGrade = false;
        PlayerStats.cityNodesData[id].isMaxLevel = false;
    }

    public void LoadBuilding()
    {

        buildingObject = PlayerStats.cityNodesData[id].buildingGameobject;
        buildingTemplate = PlayerStats.cityNodesData[id].buildingTemplate;
        totalUpgradeTime = PlayerStats.cityNodesData[id].totalUpgradeTime;
        isFirstGraded = PlayerStats.cityNodesData[id].reachedFirstGrade;
        isSecondGraded = PlayerStats.cityNodesData[id].reachedSecondGrade;
        isThirdGraded = PlayerStats.cityNodesData[id].reachedThirdGrade;

    }
}
