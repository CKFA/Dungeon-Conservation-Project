using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuildManager : MonoBehaviour
{
    public static CityBuildManager instance;
    // Start is called before the first frame update


    public GameObject buildEffect;
    public GameObject sellEffect;

    private BuildingTemplate buildingToBuild;
    private CityNode selectedNode;

    public GameObject cityShopUI;
    public GameObject notEnoughMoneyUI;
    public static GameObject storedCityShopUI;

    public GameObject cityNodeUI;
    public static GameObject storedcityNodeUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        storedCityShopUI = cityShopUI;
        storedcityNodeUI = cityNodeUI;
        storedcityNodeUI.GetComponent<CityNodeUI>().Hide();
        DeselectNode(false) ;
    }
    public bool CanBuild { get { return buildingToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >= buildingToBuild.cost; } }

    public void SelectNode(CityNode node,bool needToReset)
    {
        if(selectedNode == node && needToReset)
        {
            DeselectNode(true);
            return;
        }
        selectedNode = node;
        DeselectBuildingToBuild(); 

        cityNodeUI.GetComponent<CityNodeUI>().SetTarget(node);
    }

    public void DeselectNode(bool needToReset)
    {
        if(needToReset)
        {
            selectedNode = null;
        }
        
        cityNodeUI.GetComponent<CityNodeUI>().Hide();
    }

    public void SelectBuildingToBuild(BuildingTemplate building)
    {
        buildingToBuild = building;
        DeselectNode(true);
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
