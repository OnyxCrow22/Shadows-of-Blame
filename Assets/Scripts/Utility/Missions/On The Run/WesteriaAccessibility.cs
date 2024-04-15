using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WesteriaAccessibility : MonoBehaviour
{
    public OnTheRun OTR;
    public bool warning;

    private void OnTriggerEnter(Collider other)
    {
        if (!OTR.canAccessWesteria && other.CompareTag("Player"))
        {
            warning = true;
            OTR.warningHolder.SetActive(true);
            OTR.warningText.text = "WARNING! You are entering a restricted travel zone! Please turn back immediately, or face the consequences!";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!OTR.canAccessWesteria)
        {
            warning = false;
            OTR.warningText.text = "";
            OTR.warningHolder.SetActive(false);
        }
    }
}
