using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColour;
    public Color warningColour;
    public Vector3 positionOffset;
    [HideInInspector]
    public GameObject tower;
    [HideInInspector]
    public TowerTemplate towerTemplate;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColour;
    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }


    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // if hovering the UI
            return;

        if (!buildManager.CanBuild) // 
        return;

        if (tower != null)
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
            return;

        if (tower != null) 
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTower(buildManager.GetTowerToBuild());
    }

    void BuildTower(TowerTemplate template)
    {
        if (PlayerStats.money < template.cost)
        {
            Debug.Log("Not Enough Money");
            return;
        }

        PlayerStats.money -= template.cost;
        GameObject _tower = (GameObject)Instantiate(template.Prefabs, GetBuildPosition(), Quaternion.identity); //build tower
        tower = _tower;

        towerTemplate = template;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTower()
    {
        if (PlayerStats.money < towerTemplate.upgradeCost)
        {
            Debug.Log("Not Enough Money");
            return;
        }

        PlayerStats.money -= towerTemplate.upgradeCost;

        Destroy(tower); // destory the old tower
        GameObject _tower = (GameObject)Instantiate(towerTemplate.upgradedPrefabs, GetBuildPosition(), Quaternion.identity); //build tower
        tower = _tower;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
    }

    public void SellTower()
    {
        PlayerStats.money += towerTemplate.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(tower);
        towerTemplate = null;
    }
}
