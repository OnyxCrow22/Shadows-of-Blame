using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthLoss;
    public float deadDuration;
    public bool isDead = false;
    public NPCMovementSM nsm;
    public Collider hitbox;

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
            isDead = true;
            nsm.police.killedNPCS += 1;
            nsm.police.cancelPursuit = false;
            PoliceLevel.activateLevel = true;
            nsm.police.UpdateLevel();
            StartCoroutine(NPCDeath());
        }
    }

    public IEnumerator NPCDeath()
    {
        nsm.NPCAnim.SetBool("dead", true);
        hitbox.enabled = false;
        yield return new WaitForSeconds(deadDuration);
        nsm.gameObject.AddComponent<RemoveNPC>();
    }

    
}
