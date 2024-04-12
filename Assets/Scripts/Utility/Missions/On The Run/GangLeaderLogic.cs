using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangLeaderLogic : MonoBehaviour
{
    public EnemyMovementSM esm;
    public OnTheRun OTR;
    public bool isDead = false;

    public void Check()
    {

        if (esm.eHealth.health == 0)
        {
            isDead = true;
            OTR.subObjective.text = "";
            OTR.objective.text = "Take the evidence from the gang leader.";
        }    
    }
}
