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
    GameObject[] police;
    public bool evadedPolice;

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
        PoliceLevel.policeLevels = 1;
        PoliceLevel.activateLevel = true;

        if (police == null || police.Length == 0)
        {
            WW.objective.text = "Go to 22 Kensington Boulevard.";
            WW.locationClues[0].text = "It's located NORTHEAST of Halifax Park.";
            WW.locationClues[1].text = "The building has a pool on the roof.";
            WW.locationClues[2].text = "The building can be seen from the M 150.";
            PoliceLevel.activateLevel = false;
            WW.evadedPolice = true;
            evadedPolice = true;
            PoliceLevel.policeLevels = 0;
        }
    }
}