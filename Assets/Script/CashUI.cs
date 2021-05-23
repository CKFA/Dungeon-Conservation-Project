using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CashUI : MonoBehaviour
{
    public Text cashText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cashText.text = "$" + PlayerStats.money.ToString();
    }
}
