using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WWCollectPrescottEvidence : MonoBehaviour
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
        WW.PrescottEvidenceCollected += 1;
        panel.SetActive(false);
        evidenceText.enabled = false;
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
            WW.objective.text = "Go to the gang compound in Northby.";
        }
    }
}
