using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWSafehouseCheck : MonoBehaviour
{
    public WestralWoes WW;
    public bool hasLeft = false;

    private void Update()
    {
        if (hasLeft)
        {
            CheckPolice();
        }
    }

    void CheckPolice()
    {
        if (PoliceLevel.policeLevels >= 1)
        {
            WW.objective.text = "Lose the police.";
        }
        if (WW.police.cancelPursuit)
        {
            WW.objective.text = "Go to Westeria Island.";
        }
    }
    void OnTriggerExit()
    {
        WW.inSafehouse = false;
        WW.objective.text = "Go to Westeria Island.";
        hasLeft = true;
        WW.inSafehouse = false;
    }
}
