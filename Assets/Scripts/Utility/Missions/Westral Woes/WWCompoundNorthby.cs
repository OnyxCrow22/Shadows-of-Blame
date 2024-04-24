using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWCompoundNorthby : MonoBehaviour
{
    public WestralWoes WW;
    public bool inCompound = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inCompound = true;
            WW.inNorthbyCompound = true;
            WW.objective.text = "Kill the gang leader.";
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
}
