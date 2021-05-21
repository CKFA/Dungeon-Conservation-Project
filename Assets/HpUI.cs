using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [HideInInspector]
    public bool isEnabled = true;
    public Text hpText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            hpText.text = "♥ × " + PlayerStats.hp.ToString();
        }
        else
        {
            hpText.text = "";
        }
    }
}
