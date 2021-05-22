﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    // Start is called before the first frame update


    public GameObject buildEffect;
    public GameObject sellEffect;

    private static TowerTemplate towerToBuild;
    private Node selectedNode;

    public GameObject shopUI ;
    public static GameObject storedShopUI;
    public GameObject notEnoughMoneyUI;

    public GameObject nodeUI;
    public static GameObject storedNodeUI;
    public GameObject rangeGameObject;

    private void Awake()
    {
        instance = this;
        storedShopUI = shopUI;
        storedNodeUI = nodeUI;
        storedNodeUI.GetComponent<NodeUI>().Hide();
        DeselectNode(true);
        if (rangeGameObject == null)
        {
            rangeGameObject = FindObjectOfType<RangeArea>().gameObject;
        }
    }

    public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >=towerToBuild.cost; } }

    public void SelectNode(Node node,bool needToReset)
    {
        if (selectedNode == node && needToReset)
        {
            DeselectNode(true);
            return;
        }
        selectedNode = node;
        DeselectTowerToBuild();

        rangeGameObject.GetComponent<RangeArea>().TurnOn(node);
        storedNodeUI.GetComponent<NodeUI>().SetTarget(node);
    }

    public void DeselectNode(bool needToReset)
    {
        rangeGameObject.GetComponent<RangeArea>().TurnOff();
        if (needToReset)
        {
            selectedNode = null;
        }

        storedNodeUI.GetComponent<NodeUI>().Hide();
    }
    public void SelectTowerToBuild(TowerTemplate tower)
    {
        towerToBuild = tower;
        //Debug.Log(towerToBuild.Prefabs.name + " from BuildManager" );
        DeselectNode(true);
    }

    public TowerTemplate GetTowerToBuild()
    {
        return towerToBuild;
    }

    public TowerTemplate DeselectTowerToBuild() // deselect the tower which selecting to build
    {
        return towerToBuild = null;
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
