using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GangEvidenceCollect : MonoBehaviour
{
    public GameObject gEvidence;
    public GameObject gPanel;
    public GameObject coWorker;
    public bool isgReading = false;
    public static bool evidence = false;
    public bool Escaped;
    public OnTheRun OTR;
    public RaycastMaster rMaster;
    GameObject[] police;

    public void GEPickup()
    {
        gPanel.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void GECloseWindow()
    {
        gPanel.SetActive(false);
        gEvidence.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        isgReading = false;
        rMaster.interactKey.SetActive(false);
        PoliceLevel.policeLevels += 1;
        PoliceLevel.activateLevel = true;
        evidence = true;
        OTR.GangEvidence = true;
        OTR.objective.text = "Lose the police.";

        if (police == null || police.Length == 0)
        {
            OTR.objective.text = "Go to your safehouse.";
            PoliceLevel.activateLevel = false;
            OTR.Escaped = true;
            Escaped = true;
            PoliceLevel.policeLevels = 0;
        }
    }
}