using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceCheck : MonoBehaviour
{
    public OnTheRun OTR;
    public PoliceLevel police;
    public bool Escaped;

    // Update is called once per frame
    void Update()
    {
        CheckEscape();
    }

    public void CheckEscape()
    {
        if (PoliceLevel.levelStage == 0 && GangEvidenceCollect.evidence)
        {
            Escaped = true;
            OTR.objective.text = "Go to your safehouse.";
            OTR.Escaped = true;
        }
    }
}
