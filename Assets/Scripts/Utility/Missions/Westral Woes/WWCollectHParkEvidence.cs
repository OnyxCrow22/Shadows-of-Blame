using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WWCollectHParkEvidence : MonoBehaviour
{
    public GameObject evidence;
    public GameObject panel;
    public GameObject clueText;
    public TextMeshProUGUI evidenceText;
    public bool reading = false;
    public WestralWoes WW;
    public RaycastMaster rMaster;

    public void PickUp()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        panel.SetActive(true);
        evidenceText.enabled = true;
    }

    public void CloseWindow()
    {
        WW.HaliEvidenceCollected += 1;
        panel.SetActive(false);
        evidenceText.enabled = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        reading = false;
        clueText.SetActive(true);
        WW.clue.SetActive(true);
        rMaster.interactKey.SetActive(false);
        evidence.SetActive(false);

        WW.objective.text = "Search Halifax Park for evidence: " + WW.HaliEvidenceCollected + " / " + WW.HaliEvidenceTotal;

        if (WW.HaliEvidenceCollected == WW.HaliEvidenceTotal)
        {
            WW.CollectedEvidenceHPark = true;
            WW.clue.SetActive(false);
            WW.HParkHolder.SetActive(false);
            WW.objective.text = "Go to Prescott.";
            WW.locationClues[0].text = "It's located in the SOUTHEAST of WEST INSBURY.";
            WW.locationClues[1].text = "The casino by the M150 is part of Prescott.";
            WW.locationClues[2].text = "The district is next to the PORT OF WEST INSBURY.";
        }
    }
}
