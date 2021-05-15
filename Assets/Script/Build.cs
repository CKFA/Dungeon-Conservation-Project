using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    private int selectedIndex;
    private GameObject currentSelectedObject;

    [SerializeField]
    private GameObject[] selectableObjects;

    private bool isAnObjectSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        selectedIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 spawnPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));

        if (Input.GetMouseButtonDown(1) && isAnObjectSelected == false) 
        {
            currentSelectedObject = (GameObject)Instantiate(selectableObjects[selectedIndex], spawnPos, Quaternion.identity);
            isAnObjectSelected = true;
        }

        else if (Input.GetMouseButtonDown(1) && isAnObjectSelected == true)
        {
            Destroy(currentSelectedObject);
            isAnObjectSelected = false;
            selectedIndex = 0;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && isAnObjectSelected)
        {
            selectedIndex++;

            if (selectedIndex > selectableObjects.Length - 1)
            {
                selectedIndex = 0;
            }

            Destroy(currentSelectedObject);
            currentSelectedObject = (GameObject)Instantiate(selectableObjects[selectedIndex], spawnPos, Quaternion.identity);
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && isAnObjectSelected)
        {
            selectedIndex--;

            if (selectedIndex < 0)
            {
                selectedIndex = selectableObjects.Length - 1;
            }

            Destroy(currentSelectedObject);
            currentSelectedObject = (GameObject)Instantiate(selectableObjects[selectedIndex], spawnPos, Quaternion.identity);
        }
    }
}
