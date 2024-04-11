using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangCompoundCheck : MonoBehaviour
{
    public OnTheRun OTR;
    public bool arrivedAtCompound = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            arrivedAtCompound = true;
            OTR.objective.text = "Kill all enemies: " + OTR.gangMembersKilled + " / " + OTR.gangMemberCount;
            OTR.subObjective.text = "Kill the Gang Leader";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        arrivedAtCompound = false;
        OTR.objective.text = "Go back to the gang compound.";
    }
}
