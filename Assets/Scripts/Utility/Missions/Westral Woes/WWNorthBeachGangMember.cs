using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWNorthBeachGangMember : MonoBehaviour
{
    public WestralWoes WW;
    public EnemyMovementSM esm;
    public bool isDead = false;
    public bool enemiesDead = false;
    public WWNorthBeachEvidence NEvidence;
    public Collider hitBox;

    public void OnDeath()
    {
        if (esm.eHealth.health <= 0)
        {
            WW.NorthBeachGangEliminated += 1;
            isDead = true;
            hitBox.enabled = false;
        }

        if (WW.NorthBeachGangEliminated == WW.NorthBeachGangAmount)
        {
            WW.objective.text = "Take the evidence from one of the gang members";
            WW.allNorthBeachGangsters = true;

            int RandomIndex = Random.Range(0, WW.NorthBeachGang.Length);
            WW.NorthBeachGang[RandomIndex].GetComponent<WWNorthBeachEvidence>().gameObject.SetActive(true);
        }
    }
}
