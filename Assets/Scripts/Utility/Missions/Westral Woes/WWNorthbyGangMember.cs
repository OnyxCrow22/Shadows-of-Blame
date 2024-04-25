using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWNorthbyGangMember : MonoBehaviour
{
    public WestralWoes WW;
    public DeadCheck dead;
    public EnemyMovementSM esm;
    public bool isDead = false;
    public bool enemiesDead = false;
    
    public void OnDeath()
    {
        if (esm.eHealth.health <= 0)
        {
            WW.NorthbyGangEliminated += 1;
            isDead = true;

            WW.objective.text = "Kill the remaining enemies: " + WW.NorthbyGangEliminated + " / " + WW.NorthbyGangAmount;
            gameObject.SetActive(false);

            if (WW.NorthbyGangEliminated == WW.NorthbyGangAmount)
            {
                WW.objective.text = "Take the evidence from the gang leader.";
                enemiesDead = true;
                WW.allNorthby = true;
            }
        }
    } 
}
