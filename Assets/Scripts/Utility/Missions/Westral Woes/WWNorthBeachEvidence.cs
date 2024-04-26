using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WWNorthBeachEvidence : MonoBehaviour
{
    public GameObject gEvidence;
    public GameObject gPanel;
    public GameObject coWorker;
    public TextMeshProUGUI coWorkerText;
    public bool isgReading = false;
    public static bool evidence = false;
    public WestralWoes WW;
    public RaycastMaster rMaster;

    public void GEPickup()
    {
        gPanel.SetActive(true);
        coWorkerText.enabled = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void GECloseWindow()
    {
        gPanel.SetActive(false);
        gEvidence.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        coWorkerText.enabled = false;
        isgReading = false;
        rMaster.interactKey.SetActive(false);
        evidence = true;
        WW.collectedNorthBeachEvidence = true;
        WW.objective.text = "Lose the police.";
    }
}