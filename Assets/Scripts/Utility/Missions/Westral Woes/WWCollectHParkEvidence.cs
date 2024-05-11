using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WWCollectHParkEvidence : MonoBehaviour
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
        WW.HaliEvidenceCollected += 1;
        panel.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        reading = false;
        clueText.SetActive(true);
        WW.clue.SetActive(true);
        WW.magGlass.SetActive(true);
        rMaster.interactKey.SetActive(false);
        evidence.SetActive(false);

        WW.objective.text = "Search Halifax Park for evidence: " + WW.HaliEvidenceCollected + " / " + WW.HaliEvidenceTotal;

        if (WW.HaliEvidenceCollected == WW.HaliEvidenceTotal)
        {
            WW.CollectedEvidenceHPark = true;
            WW.clue.SetActive(false);
            WW.magGlass.SetActive(false);
            WW.HParkHolder.SetActive(false);
            WW.objective.text = "Go to Prescott.";
            WW.locationClues[0].text = "It's located in the SOUTHEAST of WEST INSBURY.";
            WW.locationClues[1].text = "The Roulette Hotel & Casino is the tallest building in the city.";
            WW.locationClues[2].text = "The district is next to the PORT OF WEST INSBURY.";
        }
    }
}
