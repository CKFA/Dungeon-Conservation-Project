using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColour;

    private GameObject cannon;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // if hovering the UI
            return;

        if (buildManager.GetCannonToBuild() == null) // 
            return;
        rend.material.color = hoverColour;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColour;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // if hovering the UI
            return;

        if (buildManager.GetCannonToBuild() == null)
            return;

        if (cannon != null) 
        {
            Debug.Log("Can't Build there - ToDo: Display to the screen");
            return;
        }

        //build a cannon
        GameObject cannonToBuild = buildManager.GetCannonToBuild();
        cannon = (GameObject)Instantiate(cannonToBuild, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation);
    }
}
