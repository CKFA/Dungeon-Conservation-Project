using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuildManager : MonoBehaviour
{
    public static CityBuildManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private BuildingTemplate buildingToBuild;
    private CityNode selectedNode;

    public CityShop cityShopUI;
    public GameObject notEnoughMoneyUI;
    public CityNodeUI cityNodeUI;
    //public RangeArea rangeArea;

    public bool CanBuild { get { return buildingToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >= buildingToBuild.cost; } }

    public void SelectNode(CityNode node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        DeselectBuildingToBuild(); 

        cityNodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        cityNodeUI.Hide();
    }

    public void SelectBuildingToBuild(BuildingTemplate building)
    {
        buildingToBuild = building;
        DeselectNode();
    }

    public BuildingTemplate GetBuildingToBuild()
    {
        return buildingToBuild;
    }

    public BuildingTemplate DeselectBuildingToBuild() // deselect the tower which selecting to build
    {
        return buildingToBuild = null;
    }

    public void NotEnoughMoney() // ****************************** Bug
    {
        StartCoroutine(ShowingNoMoneyUI());
        notEnoughMoneyUI.SetActive(false);
    }

    IEnumerator ShowingNoMoneyUI()
    {
        notEnoughMoneyUI.SetActive(true);
        Debug.Log("Not Enough Money!");
        yield return new WaitForSeconds(5f);
    }
}
