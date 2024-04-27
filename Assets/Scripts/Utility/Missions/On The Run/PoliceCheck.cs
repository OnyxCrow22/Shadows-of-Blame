using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceCheck : MonoBehaviour
{
    public OnTheRun OTR;
    public PoliceLevel police;
    public bool Escaped;

    public void PoliceIntialise()
    {
        PoliceLevel.policeLevels += 1;
        PoliceLevel.activateLevel = true;
        OTR.objective.text = "Lose the police.";
    }

    public void NoPolice()
    {
        OTR.objective.text = "Go to your safehouse.";
    }
}
