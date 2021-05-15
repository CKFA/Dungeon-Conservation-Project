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

    public GameObject standardCannonPrefab;

    public GameObject LaserPrefab;

    private GameObject CannonToBuild;
    public GameObject GetCannonToBuild()
    {
        return CannonToBuild;
    }

    public void SetCannonToBuild(GameObject Cannon)
    {
        CannonToBuild = Cannon;
    }

    public GameObject rocketLauncher;



}
