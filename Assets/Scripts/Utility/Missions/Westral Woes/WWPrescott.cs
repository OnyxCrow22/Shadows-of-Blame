using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWPrescott : MonoBehaviour
{
    public WestralWoes WW;
    public bool inPrescott = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPrescott = true;
            WW.inPrescott = true;
            WW.objective.text = "Search for more evidence in Prescott: " + WW.PrescottEvidenceCollected + " / " + WW.PrescottEvidenceTotal;
        }
    }
}
