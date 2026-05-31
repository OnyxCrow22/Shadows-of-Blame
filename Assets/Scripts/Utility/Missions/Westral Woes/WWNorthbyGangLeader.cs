using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWNorthbyGangLeader : MonoBehaviour
{
    public EnemyMovementSM esm;
    public WestralWoes WW;
    public WWNorthbyGangEvidence northbyEvidence;
    public bool isDead = false;

    public void Check()
    {
        if (esm.eHealth.health == 0)
        {
            isDead = true;
            WW.northbyLeaderdown = true;

            if (WW.NorthbyGangEliminated < WW.NorthbyGangAmount)
            {
                WW.subObjective.text = "";
                WW.objective.text = "Kill the gang members: " + WW.NorthbyGangEliminated + " / " + WW.NorthbyGangAmount;
            }

            else if (WW.NorthbyGangEliminated == WW.NorthbyGangAmount && WW.northbyLeaderdown)
            {
                WW.subObjective.text = "";
                WW.objective.text = "Take the evidence from the gang leader.";
                WW.allNorthby = true;
                if (WW.allNorthby)
                {
                    northbyEvidence.gEvidence.SetActive(true);
                }
            }
        }    
    }
}
