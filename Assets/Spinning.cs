using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    [Header("Spinning")]
    public bool isClockwise = false;
    public bool spinX = false;
    public bool spinY = false;
    public bool spinZ = false;
    public float speed;

    void Update()
    {
        if (spinX && !isClockwise)
            transform.Rotate(new Vector3(Time.deltaTime * speed, 0, 0));
        if (spinY && !isClockwise)
            transform.Rotate(new Vector3(0, Time.deltaTime * speed, 0));
        if (spinZ && !isClockwise)
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed));
        if (spinX && isClockwise)
            transform.Rotate(new Vector3(-Time.deltaTime * speed, 0, 0));
        if (spinY && isClockwise)
            transform.Rotate(new Vector3(0, -Time.deltaTime * speed, 0));
        if (spinZ && isClockwise)
            transform.Rotate(new Vector3(0, 0, -Time.deltaTime * speed));
    }
}
