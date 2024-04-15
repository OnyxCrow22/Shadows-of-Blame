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
        PoliceLevel.levelStage += 1;
        PoliceLevel.giveLevel = true;
        OTR.objective.text = "Lose the police.";
    }

    public void NoPolice()
    {
        OTR.objective.text = "Go to your safehouse.";
    }
}
