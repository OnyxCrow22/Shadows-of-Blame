using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangLeaderLogic : MonoBehaviour
{
    public EnemyMovementSM esm;
    public OnTheRun OTR;
    public GangEvidenceCollect GECollect;
    public bool isDead = false;

    public void Check()
    {

        if (esm.eHealth.health == 0)
        {
            isDead = true;

            if (OTR.enemies.Length > 0)
            {
                OTR.subObjective.text = "";
                OTR.objective.text = "Kill the remaining enemies: " + OTR.gangMembersKilled + " / " + OTR.gangMemberCount;
            }

            else if (OTR.enemies.Length <= 0)
            {
                OTR.subObjective.text = "";
                OTR.objective.text = "Take the evidence from the gang leader.";
                OTR.EliminatedGang = true;
                if (OTR.EliminatedGang)
                {
                    GECollect.gEvidence.SetActive(true);
                }
            }
        }    
    }
}
