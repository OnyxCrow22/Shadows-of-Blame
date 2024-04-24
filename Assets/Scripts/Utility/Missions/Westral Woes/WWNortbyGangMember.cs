using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWNorthbyGangMember : MonoBehaviour
{
    public OnTheRun OTR;
    public EnemyMovementSM esm;
    public bool isDead = false;
    public bool enemiesDead = false;
    public Collider capsule;
    
    public void OnDeath()
    {
        if (esm.eHealth.health == 0)
        {
            OTR.gangMembersKilled += 1;
            isDead = true;

            OTR.objective.text = "Kill the remaining enemies: " + OTR.gangMembersKilled + " / " + OTR.gangMemberCount;
            capsule.enabled = false;

            if (OTR.gangMembersKilled == OTR.gangMemberCount && OTR.EliminatedGang)
            {
                OTR.objective.text = "Take the evidence from the gang leader.";
                enemiesDead = true;
                OTR.allEnemiesKilled = true;
            }
        }
    } 
}
