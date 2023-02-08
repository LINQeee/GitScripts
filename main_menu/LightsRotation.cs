using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsRotation : MonoBehaviour
{
    float increaseNumber = 0.1f;
    void Update()
    {
        transform.localEulerAngles += new Vector3(0, increaseNumber, 0);
        if(transform.localEulerAngles.y > 140 || transform.localEulerAngles.y < -140) increaseNumber *= -1;
    }
}
