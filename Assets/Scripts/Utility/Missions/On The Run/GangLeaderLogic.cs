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
            OTR.gangLeaderdead = true;

            if (OTR.gangMembersKilled < OTR.gangMemberCount)
            {
                OTR.subObjective.text = "";
                OTR.objective.text = "Kill the gang members: " + OTR.gangMembersKilled + " / " + OTR.gangMemberCount;
            }

            else if (OTR.gangMembersKilled == OTR.gangMemberCount && OTR.gangLeaderdead)
            {
                OTR.subObjective.text = "";
                OTR.objective.text = "Take the evidence from the gang leader.";
                OTR.EliminatedGang = true;
                OTR.allenemiesKilled = true;
                if (OTR.allenemiesKilled)
                {
                    GECollect.gEvidence.SetActive(true);
                }
            }
        }
    }
}
