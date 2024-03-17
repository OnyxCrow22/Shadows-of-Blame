using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthLoss;
    public float deadDuration;
    private EnemyMovementSM esm;
    public GameObject enemy;

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
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deadDuration);
        Destroy(enemy.gameObject);
    }
}
