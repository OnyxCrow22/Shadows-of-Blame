using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WestralSquareCheck : MonoBehaviour
{
    public OnTheRun OTR;
    public bool WSquare = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WSquare = true;
            OTR.objective.text = "Search Westral Square for evidence: " + OTR.collectedEvidence + " / " + OTR.totalEvidence;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!OTR.Evidence)
        {
            WSquare = false;
            OTR.objective.text = "Go back to Westral Square.";
        }
    }
}
