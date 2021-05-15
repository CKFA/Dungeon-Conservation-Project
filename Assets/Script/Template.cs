using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Template : MonoBehaviour
{
    [SerializeField]
    private GameObject finalObject;
    private Vector2 mousePos;
    public Tilemap pathTileMap;
    public Tilemap groundTileMap;
    [SerializeField]
    private LayerMask AllTilesLayers;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.zero,Mathf.Infinity,AllTilesLayers);

            if (rayHit.collider == null && CheckTileIsExist(mousePos))
            {
                Instantiate(finalObject, transform.position, Quaternion.identity);
            }
            

        }

    }

    bool CheckTileIsExist(Vector3 _mousePos)
    {
        if (groundTileMap.GetTile(groundTileMap.WorldToCell(_mousePos)) != null)
        {
            return true;
        }
        else
            return false;
    }
}
