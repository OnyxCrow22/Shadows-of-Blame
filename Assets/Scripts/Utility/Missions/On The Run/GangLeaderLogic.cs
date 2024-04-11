using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangLeaderLogic : MonoBehaviour
{
    public EnemyHealth leaderHealth;
    public OnTheRun OTR;
    public bool isDead;

    public void CheckForDeath()
    {
        if (leaderHealth.health <= 0 && !isDead)
        {
            isDead = true;
            OTR.GangLeader = true;
            OTR.subObjective.text = "";
            OTR.objective.text = "Take the evidence from the gang leader.";
        }
    }
}
