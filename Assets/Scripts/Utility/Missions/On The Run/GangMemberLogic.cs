using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangMemberLogic : MonoBehaviour
{
    public OnTheRun OTR;
    public EnemyMovementSM esm;
    public bool isDead = false;
    public bool enemiesDead = false;
    public Collider capsule;
    public GangLeaderLogic leader;
    
    public void OnDeath()
    {
        if (esm.eHealth.health <= 0)
        {
            OTR.gangMembersKilled += 1;
            isDead = true;
            capsule.enabled = false;
        }

        if (OTR.gangLeaderdead)
        {
            OTR.objective.text = "Kill the gang members: " + OTR.gangMembersKilled + " / " + OTR.gangMemberCount;
            OTR.subObjective.text = "";
        }
        else if (!OTR.gangLeaderdead)
        {
            OTR.objective.text = "Kill the gang leader.";
            OTR.subObjective.text = "Kill the gang members: " + OTR.gangMembersKilled + " / " + OTR.gangMemberCount;
        }

        if (OTR.gangMembersKilled == OTR.gangMemberCount && !OTR.gangLeaderdead)
        {
            OTR.objective.text = "Kill the gang leader.";
            OTR.subObjective.text = "";
        }

        else if (OTR.gangMembersKilled == OTR.gangMemberCount && OTR.gangLeaderdead)
        {
            OTR.subObjective.text = "";
            OTR.objective.text = "Take the evidence from the gang leader.";
            OTR.EliminatedGang = true;
            OTR.allenemiesKilled = true;

            if (OTR.allenemiesKilled)
            {
                leader.GECollect.gEvidence.SetActive(true);
            }
        }
    } 
}
