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
    public GameObject towerObject;
    [HideInInspector]
    public TowerTemplate towerTemplate;
    public BuildingTemplate buildingTemplate;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColour;

    BuildManager buildManager;

    public NodeData nodeData;

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
            return;

        if (towerObject != null) // if the tower built
        {
            buildManager.SelectNode(this); // select this node
            return;
        }

        if (!buildManager.CanBuild) // if can't build
            return;

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


        // save
        if(string.IsNullOrEmpty(nodeData.id))
        {
            nodeData.id = System.DateTime.Now.ToLongDateString() + System.DateTime.Now.ToLongTimeString() + this.name;
            nodeData.towerType = template.Prefabs.name;
            nodeData.position = template.Prefabs.transform.position;
            nodeData.quaternion = template.Prefabs.transform.rotation;
            SaveData.current.nodeData
        }
        

        towerTemplate = template;

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

        PlayerStats.money -= towerTemplate.damageUpgradeCost;
        
        towerObject.GetComponent<Tower>().ChangeColorChecker();


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
            PlayerStats.money -= towerTemplate.rangeUpgradeCost;

            towerObject.GetComponent<Tower>().ChangeColorChecker();

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
            PlayerStats.money -= towerTemplate.rateUpgradeCost;

            towerObject.GetComponent<Tower>().ChangeColorChecker();

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
}
