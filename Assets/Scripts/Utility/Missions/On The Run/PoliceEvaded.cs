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
        if (PoliceLevel.policeLevels >= 1)
        {
            PoliceLevel.activateLevel = true;
            police.UpdateLevel();
        }
    }
}
