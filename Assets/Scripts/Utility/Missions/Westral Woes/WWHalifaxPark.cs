using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWHalifaxPark : MonoBehaviour
{
    public WestralWoes WW;
    public bool HPark = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HPark = true;
            WW.HaliPark = true;
            WW.objective.text = "Search for evidence in Halifax Park: " + WW.HaliEvidenceCollected + " / " + WW.HaliEvidenceTotal;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!WW.CollectedEvidenceHPark)
        {
            HPark = false;
            WW.HaliPark = false;
            WW.objective.text = "Go back to Halifax Park.";
        }
    }
}
