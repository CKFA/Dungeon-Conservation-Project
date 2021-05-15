using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void BuyCannon()
    {
        Debug.Log("CannonPurchased");
        buildManager.SetCannonToBuild(buildManager.standardCannonPrefab);
    }
}
