using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using TMPro;
using UnityEngine;

public class GangEvidenceCollect : MonoBehaviour
{
    public GameObject gEvidence;
    public GameObject gPanel;
    public GameObject coWorker;
    public bool isgReading = false;
    public bool evidence = false;
    public bool Escaped = false;
    public OnTheRun OTR;
    public RaycastMaster rMaster;
    public PoliceLevel police;
    public PoliceEvaded policeCheck;

    private void Start()
    {
        police.cancelPursuit = false;
    }

    public void GEPickup()
    {
        gPanel.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void GECloseWindow()
    {
        gPanel.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        isgReading = false;
        rMaster.interactKey.SetActive(false);
        evidence = true;
        OTR.GangEvidence = true;
        gEvidence.SetActive(false);
        police.UpdateLevel();
        PoliceLevel.activateLevel = true;
        OTR.objective.text = "Lose the police.";

        if (OTR.police.cancelPursuit)
        {
            policeCheck.EvadedPolice();
        }
    }
}