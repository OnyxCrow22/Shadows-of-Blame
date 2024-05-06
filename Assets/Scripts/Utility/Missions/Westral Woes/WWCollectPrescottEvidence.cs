using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WWCollectPrescottEvidence : MonoBehaviour
{
    public GameObject evidence;
    public GameObject panel;
    public GameObject clueText;
    public bool reading = false;
    public WestralWoes WW;
    public RaycastMaster rMaster;

    public void PickUp()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        panel.SetActive(true);
    }

    public void CloseWindow()
    {
        WW.PrescottEvidenceCollected += 1;
        WW.magGlass.SetActive(true);
        panel.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        reading = false;
        clueText.SetActive(true);
        WW.clue.SetActive(true);
        rMaster.interactKey.SetActive(false);
        evidence.SetActive(false);

        WW.objective.text = "Search Prescott for more evidence: " + WW.PrescottEvidenceCollected + " / " + WW.PrescottEvidenceTotal;

        if (WW.PrescottEvidenceCollected == WW.PrescottEvidenceTotal)
        {
            WW.CollectedEvidencePrescott = true;
            WW.clue.SetActive(false);
            WW.magGlass.SetActive(false);
            WW.PrescottHolder.SetActive(false);
            WW.objective.text = "Go to Northby Autos.";
            WW.locationClues[0].text = "It's located NORTH of Prescott.";
            WW.locationClues[1].text = "The auto shop is just off the M 150.";
            WW.locationClues[2].text = "It's immediately EAST of Northby Roundabout";
        }
    }
}
