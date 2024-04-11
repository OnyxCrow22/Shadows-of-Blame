using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectEvidence : MonoBehaviour
{
    public GameObject evidence;
    public GameObject panel;
    public GameObject clueText;
    public TextMeshProUGUI evidenceText;
    public bool reading = false;
    public OnTheRun OTR;
    public RaycastMaster rMaster;

    public void PickUp()
    {
        panel.SetActive(true);
        evidenceText.enabled = true;
    }

    public void CloseWindow()
    {
        OTR.collectedEvidence += 1;
        panel.SetActive(false);
        evidenceText.enabled = false;
        reading = false;
        clueText.SetActive(true);
        OTR.clue.SetActive(true);
        rMaster.interactKey.SetActive(false);
        evidence.SetActive(false);
        
        OTR.objective.text = "Search Westral Square for evidence: " + OTR.collectedEvidence + " / " + OTR.totalEvidence;

        if (OTR.collectedEvidence == OTR.totalEvidence)
        {
            OTR.Evidence = true;
            OTR.objective.text = "Go to the gang compound.";
        }
    }
}
