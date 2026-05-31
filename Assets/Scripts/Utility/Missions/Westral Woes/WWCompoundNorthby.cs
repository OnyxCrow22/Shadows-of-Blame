using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWCompoundNorthby : MonoBehaviour
{
    public WestralWoes WW;
    public bool inCompound = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && WW.CollectedEvidencePrescott || other.CompareTag("Vehicle") && WW.CollectedEvidencePrescott)
        {
            inCompound = true;
            WW.inNorthbyCompound = true;
            WW.objective.text = "Kill the gang leader.";
            WW.locationClues[0].text = "";
            WW.locationClues[1].text = "";
            WW.locationClues[2].text = "";
            if (WW.NorthbyGang.Length > 0)
            {
                WW.subObjective.text = "Kill the gang members: " + WW.NorthbyGangEliminated + " / " + WW.NorthbyGangAmount;
            }
            else if (WW.NorthbyGang.Length == 0)
            {
                WW.objective.text = "Take the evidence from the gang leader.";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!inCompound && !WW.allNorthby && !WW.northbyLeaderdown && !WW.collectedNorthbyEvidence && WW.CollectedEvidencePrescott)
        {
            inCompound = false;
            WW.objective.text = "Go back to Northby Autos.";
        }
    }
}
