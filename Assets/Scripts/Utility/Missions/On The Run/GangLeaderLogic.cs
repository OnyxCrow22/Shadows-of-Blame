using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangLeaderLogic : MonoBehaviour
{
    public EnemyMovementSM esm;
    public EnemyHealth leaderHealth;
    public OnTheRun OTR;
    public bool isDead = false;

    public void CheckForDeath()
    {
        if (leaderHealth.healthLoss <= 0)
        {
            isDead = true;
            OTR.subObjective.text = "";
            OTR.objective.text = "Take the evidence from the gang leader.";
        }
    }
}
