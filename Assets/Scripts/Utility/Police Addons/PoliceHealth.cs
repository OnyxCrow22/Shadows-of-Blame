using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthLoss;
    public float deadDuration;
    public bool isDead = false;
    public PoliceMovementSM police;
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
            police.policing.killedOfficers += 1;
            police.policing.cancelPursuit = false;
            PoliceLevel.activateLevel = true;
            police.policing.UpdateLevel();
            StartCoroutine(PoliceDeath());
        }
    }

    public IEnumerator PoliceDeath()
    {
        police.PoliceAnim.SetBool("dead", true);
        hitbox.enabled = false;
        yield return new WaitForSeconds(deadDuration);
        this.AddComponent<RemoveNPC>();
    }

    
}
