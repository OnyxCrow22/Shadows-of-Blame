using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWSafehouseCheck : MonoBehaviour
{
    public WestralWoes WW;
    public bool hasLeft = false;

    void OnTriggerExit()
    {
        WW.inSafehouse = false;
        WW.objective.text = "Go to Westeria Island.";
        hasLeft = true;
        WW.inSafehouse = false;
    }
}
