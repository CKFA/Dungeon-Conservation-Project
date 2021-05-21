using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TowerTemplate towerToBuild;
    private Node selectedNode;

    public Shop shopUI;
    public GameObject notEnoughMoneyUI;
    public NodeUI nodeUI;
    public RangeArea rangeArea;

    public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >=towerToBuild.cost; } }

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        DeselectTowerToBuild();

        rangeArea.Enabled(node);
        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        rangeArea.Disabled();
        selectedNode = null;
        nodeUI.Hide();
    }
    public void SelectTowerToBuild(TowerTemplate tower)
    {
        towerToBuild = tower;
        DeselectNode();
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
