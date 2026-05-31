using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthLoss;
    public float deadDuration;
    public GameObject enemy;
    public EnemyMovementSM esm;

    private void Start()
    {
        maxHealth = health;
    }

    public void LoseHealth(float healthLoss)
    {
        health -= healthLoss;

        if (health <= 0)
        {
            health = 0;
            maxHealth = 0;
            esm.isDealDamage = false;
            StartCoroutine(Death());
        }

        if (enemy.CompareTag("SaintMarysGangMember"))
        {
            esm.GMLogic.OnDeath();
        }

        if (enemy.CompareTag("SaintMarysGangLeader"))
        {
            esm.GGLogic.Check();
        }

        if (enemy.CompareTag("NorthbyGangLeader"))
        {
            esm.northbyLeader.Check();
        }

        if (enemy.CompareTag("NorthbyGangMember"))
        {
            esm.northbyGang.OnDeath();
        }

        if (enemy.CompareTag("NorthBeachGangMember"))
        {
            esm.northBeachGang.OnDeath();
        }
    }

    public IEnumerator Death()
    {
        esm.eAnim.SetBool("dead", true);
        yield return new WaitForSeconds(deadDuration);
       // Destroy(enemy.gameObject);
    }
}
