using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceEvaded : MonoBehaviour
{
    public PoliceLevel police;
    public OnTheRun OTR;
    public GangEvidenceCollect GECollect;
    public bool lostPolice = false;

    public void EvadedPolice()
    {
        OTR.objective.text = "Go to your safehouse.";
        OTR.Escaped = true;
        GECollect.Escaped = true;
        lostPolice = true;
    }
}
