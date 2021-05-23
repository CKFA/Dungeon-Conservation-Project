using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CityNode : MonoBehaviour
{
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
    CityBuildManager cityBuildManager;

    [Header("Upgrade")]

    private int firstGradeTime = 0;
    private int secondGradeTime = 0;
    private int thirdGradeTime = 0;

    public bool isFirstGraded = false;
    public bool isSecondGraded = false;
    public bool isThirdGraded = false;

    public int upgradeTime = 0;
    [HideInInspector]
    public bool isMaxed = false;

    // Start is called before the first frame update
    void Start()
    {
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

        if (!cityBuildManager.CanBuild) // 
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
            CityBuildManager.instance.NotEnoughMoney();
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
            BuildManager.instance.NotEnoughMoney();
            return;
        }

        PlayerStats.money -= template.cost;
        GameObject _building = (GameObject)Instantiate(template.Prefabs, GetBuildPosition(), Quaternion.identity); //build building
        buildingObject = _building;

        buildingTemplate = template;

        firstGradeTime = GetBuildingComponent().firstGradeTime;
        secondGradeTime = GetBuildingComponent().secondGradeTime;
        thirdGradeTime = GetBuildingComponent().thirdGradeTime;

        GameObject effect = (GameObject)Instantiate(cityBuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void DestroyBuilding()
    {
        PlayerStats.money += buildingTemplate.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(cityBuildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Initialise();

        Destroy(buildingObject);
        buildingTemplate = null;
    }

    public void UpgradeBuilding()
    {
        if (PlayerStats.money < buildingTemplate.upgradeCost)
        {
            Debug.Log("Not Enough Money");
            return;
        }

        TimesUpgradeAdder(); // upgrade time ++

        PlayerStats.money -= buildingTemplate.upgradeCost;

        ColourChanger();
       
        GameObject effect = (GameObject)Instantiate(cityBuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public Renderer GetRenderer()
    {
        return rend;
    }

    public Building GetBuildingComponent( )
    {
        return buildingTemplate.Prefabs.GetComponent<Building>();
    }

    public int GradeChecker() // for check grade
    {
        if ((upgradeTime == firstGradeTime) && (!isFirstGraded))
        {
            isFirstGraded = true;
            return 1;
        }
        else if ((upgradeTime == secondGradeTime) && (!isSecondGraded))
        {
            isSecondGraded = true;
            return 2;
        }
        else if ((upgradeTime == thirdGradeTime) && (!isThirdGraded))
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
                startColour = GetBuildingComponent().firstGradedColour;
                rend.material.color = startColour;
                break;
            case 2:
                startColour = GetBuildingComponent().secondGradedColour;
                rend.material.color = startColour;
                break;
            case 3:
                startColour = GetBuildingComponent().thirdGradedColour;
                rend.material.color = startColour;
                isMaxed = true;
                break;
            default:
                break;
        }
    }
    public int TimesUpgradeChecker()
    {
        int _upgradeTime = upgradeTime;
        return _upgradeTime;
    }

    public bool TimesUpgradeAdder()
    {
        if (upgradeTime < GetBuildingComponent().maxUpgradeTime)
        {
            upgradeTime++;
            Debug.Log(this.name + " " + upgradeTime);
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

        upgradeTime = 0;
    }
}
