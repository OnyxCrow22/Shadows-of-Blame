using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GangEvidenceCollect : MonoBehaviour
{
    public GameObject gEvidence;
    public GameObject gPanel;
    public GameObject coWorker;
    public TextMeshProUGUI coWorkerText;
    public bool isgReading = false;
    public bool evidence = false;
    public OnTheRun OTR;
    public RaycastMaster rMaster;

    public void GEPickup()
    {
        gPanel.SetActive(true);
        coWorkerText.enabled = true;
    }

    public void GECloseWindow()
    {
        gPanel.SetActive(false);
        gEvidence.SetActive(false);
        coWorkerText.enabled = false;
        isgReading = false;
        rMaster.interactKey.SetActive(false);
        PoliceLevel.levelStage += 1;
        PoliceLevel.giveLevel = true;
        evidence = true;
        OTR.GangEvidence = true;
        OTR.objective.text = "Lose the police.";
    }
}
