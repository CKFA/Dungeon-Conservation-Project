using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public int startMoney;

    public static int hp;
    public int startHp = 20;

    public static int waves;

    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
        hp = startHp;
        waves = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
