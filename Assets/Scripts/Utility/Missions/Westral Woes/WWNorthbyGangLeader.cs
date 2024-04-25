using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWNorthbyGangLeader : MonoBehaviour
{
    public EnemyMovementSM esm;
    public WestralWoes WW;
    public GangEvidenceCollect GECollect;
    public bool isDead = false;

    public void Check()
    {

        if (esm.eHealth.health == 0)
        {
            isDead = true;

            if (WW.NorthbyGang.Length > 0)
            {
                WW.subObjective.text = "";
                WW.objective.text = "Kill the remaining enemies: " + WW.NorthbyGangEliminated + " / " + WW.NorthbyGangAmount;
            }

            else if (WW.NorthbyGang.Length <= 0)
            {
                WW.subObjective.text = "";
                WW.objective.text = "Take the evidence from the gang leader.";
                WW.allNorthby = true;
                if (WW.allNorthby)
                {
                    GECollect.gEvidence.SetActive(true);
                }
            }
        }    
    }
}
