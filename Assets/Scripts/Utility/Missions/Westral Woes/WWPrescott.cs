using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWPrescott : MonoBehaviour
{
    public WestralWoes WW;
    public bool inPrescott = false;
    public bool CollectedPrescottEvidence = false;

    private void Update()
    {
        if (inPrescott)
        {
            CheckPolice();
        }
    }
    void CheckPolice()
    {
        if (PoliceLevel.policeLevels >= 1)
        {
            WW.objective.text = "Lose the police.";
            WW.locationClues[0].text = "";
            WW.locationClues[1].text = "";
            WW.locationClues[2].text = "";
        }
        if (WW.police.cancelPursuit)
        {
            WW.objective.text = "Search for more evidence in Prescott: " + WW.PrescottEvidenceCollected + " / " + WW.PrescottEvidenceTotal;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && WW.CollectedEvidenceHPark || other.CompareTag("Vehicle") && WW.CollectedEvidenceHPark)
        {
            inPrescott = true;
            WW.inPrescott = true;
            WW.objective.text = "Search for more evidence in Prescott: " + WW.PrescottEvidenceCollected + " / " + WW.PrescottEvidenceTotal;
            WW.locationClues[0].text = "";
            WW.locationClues[1].text = "";
            WW.locationClues[2].text = "";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!WW.CollectedEvidencePrescott)
        {
            inPrescott = false;
            WW.inPrescott = false;
            WW.objective.text = "Go back to Prescott.";
        }
    }
}
