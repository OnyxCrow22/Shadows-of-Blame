using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangLeaderLogic : MonoBehaviour
{
    public EnemyMovementSM esm;
    public OnTheRun OTR;
    public GangEvidenceCollect GECollect;
    GameObject[] search;
    public bool isDead = false;

    public void Check()
    {

        if (esm.eHealth.health == 0)
        {
            isDead = true;

            search = GameObject.FindGameObjectsWithTag("GangMember");

            if (search == GameObject.FindGameObjectsWithTag("GangMember"))
            {
                OTR.subObjective.text = "";
                OTR.objective.text = "Kill the remaining enemies: " + OTR.gangMembersKilled + " / " + OTR.gangMemberCount;
            }

            else if (search != GameObject.FindGameObjectsWithTag("GangMember"))
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
