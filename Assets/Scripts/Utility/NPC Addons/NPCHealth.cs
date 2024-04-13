using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthLoss;
    public float deadDuration;
    public bool isDead = false;
    public NPCMovementSM nsm;

    private void Start()
    {
        maxHealth = health;
        isDead = false;
    }

    public void LoseHealth(float healthLoss)
    {
        health -= healthLoss;

        if (health <= 0)
        {
            health = 0;
            maxHealth = 0;
            StartCoroutine(NPCDeath());
        }
    }

    public IEnumerator NPCDeath()
    {
        nsm.NPCAnim.SetBool("dead", true);
        PoliceLevel.levelStage += 1;
        PoliceLevel.giveLevel = true;
        yield return new WaitForSeconds(deadDuration);
        this.AddComponent<RemoveNPC>();
    }

    
}
