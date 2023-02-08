using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerData : MonoBehaviour
{
    public int currentHeadId;
    public int currentHairId;
    public int currentFacialHairId;
    public int currentEyebrowsId;

    private void Start() {
        currentHairId = 0;
        currentHeadId = 0;
        currentFacialHairId = 0;
        currentEyebrowsId = 0;
    }
}

