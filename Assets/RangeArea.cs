using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeArea : MonoBehaviour
{
    public static RangeArea instance;
    private Node target;
    public GameObject rangeArea;
    public static GameObject storedRangeArea;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        if(storedRangeArea == null)
        {
            storedRangeArea = rangeArea;
            DontDestroyOnLoad(storedRangeArea.transform.parent.gameObject);
        }
        if(rangeArea == null)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(Node _target)
    {
        target = _target;

        storedRangeArea.SetActive(true);
        storedRangeArea.transform.position = target.transform.position;
        storedRangeArea.transform.localScale = new Vector3(_target.towerObject.GetComponent<Tower>().range, _target.towerObject.GetComponent<Tower>().range, _target.towerObject.GetComponent<Tower>().range);
        
    }

    public void Hide()
    {
        storedRangeArea.SetActive(false);
    }
}
