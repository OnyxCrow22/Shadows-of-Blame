using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WWNorthbyGangEvidence : MonoBehaviour
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
        WW.collectedNorthbyEvidence = true;
        WW.objective.text = "Go to North Beach.";
        WW.locationClues[0].text = "It's located WEST of Northby.";
        WW.locationClues[1].text = "The district has neon lighting at night.";
        WW.locationClues[2].text = "It's sandwiched between the M 150.";
    }
}