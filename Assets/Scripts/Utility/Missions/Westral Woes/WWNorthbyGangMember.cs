using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWNorthbyGangMember : MonoBehaviour
{
    public WestralWoes WW;
    public EnemyMovementSM esm;
    public bool isDead = false;
    public bool enemiesDead = false;
    public Collider hitBox;
    public WWNorthbyGangLeader leader;

    public void OnDeath()
    {
        if (esm.eHealth.health <= 0)
        {
            WW.NorthbyGangEliminated += 1;
            isDead = true;
            hitBox.enabled = false;
        }

        if (WW.northbyLeaderdown)
        {
            WW.objective.text = "Kill the gang members: " + WW.NorthbyGangEliminated + " / " + WW.NorthbyGangAmount;
            WW.subObjective.text = "";
        }
        else if (!WW.northbyLeaderdown)
        {
            WW.objective.text = "Kill the gang leader.";
            WW.subObjective.text = "Kill the gang members: " + WW.NorthbyGangEliminated + " / " + WW.NorthbyGangAmount;
        }

        if (WW.NorthbyGangEliminated == WW.NorthbyGangAmount && !WW.northbyLeaderdown)
        {
            WW.objective.text = "Kill the gang leader.";
            WW.subObjective.text = "";
        }

        if (WW.NorthbyGangEliminated == WW.NorthbyGangAmount && WW.northbyLeaderdown)
        {
            WW.objective.text = "Take the evidence from the gang leader.";
            WW.subObjective.text = "";
            WW.allNorthby = true;
            if (WW.allNorthby)
            {
                leader.northbyEvidence.gEvidence.SetActive(true);
            }
        }
    }
}
