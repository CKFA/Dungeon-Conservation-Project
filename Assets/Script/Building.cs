using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    [Header("General")]

    [Range(0f,3f)]
    public float dmgBuff = 0;
    [Range(0f, 3f)]
    public float rangeBuff = 0;
    [Range(0f, 3f)]
    public float rateBuff = 0;

    [Header("Upgrade")]

    [Range(0,10)]
    public int maxUpgradeTime = 3;
    [Header("UpgradeColour")]


    public Color firstGradedColour;
    public Color secondGradedColour;
    public Color thirdGradedColour;

    [Range(0, 10)]
    public int firstGradeTime = 1;
    [Range(0, 10)]
    public int secondGradeTime = 2;
    [Range(0, 10)]
    public int thirdGradeTime = 3;


    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, startRange);
    //}
}
