using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeArea : MonoBehaviour
{
    private Node target;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOn(Node _target)
    {
        target = _target;

        if(!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }
        transform.position = target.transform.position;
        transform.localScale = new Vector3(_target.towerObject.GetComponent<Tower>().range, _target.towerObject.GetComponent<Tower>().range, _target.towerObject.GetComponent<Tower>().range);
        
    }

    public void TurnOff()
    {
        this.gameObject.SetActive(false);
    }
}
