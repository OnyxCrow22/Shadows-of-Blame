using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangCompoundCheck : MonoBehaviour
{
    public OnTheRun OTR;
    public bool arrivedAtCompound = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !OTR.EliminatedGang)
        {
            arrivedAtCompound = true;

            if (OTR.enemies.Length <= 0)
            {
                OTR.objective.text = "Kill the gang leader.";
                OTR.InCompound = true;
                OTR.subObjective.text = "";
            }
            else if (OTR.enemies.Length > 0)
            {
                OTR.objective.text = "Kill all enemies: " + OTR.gangMembersKilled + " / " + OTR.gangMemberCount;
                OTR.subObjective.text = "Kill the Gang Leader";
                OTR.InCompound = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!OTR.EliminatedGang)
        {
            arrivedAtCompound = false;
            OTR.InCompound = false;
            OTR.objective.text = "Go back to the gang compound.";
        }
    }
}
