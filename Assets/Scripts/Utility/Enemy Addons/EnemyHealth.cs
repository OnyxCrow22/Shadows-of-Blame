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
            GetComponent<EnemyMovementSM>().isDealDamage = false;
            StartCoroutine(Death());
        }
    }

    public void ResetAttack()
    {
        if (GetComponent<EnemyMovementSM>().isMeleeAttack == true)
        {
            StartCoroutine(ResetPunch());
        }
    }

    IEnumerator ResetPunch()
    {
        GetComponent<EnemyMovementSM>().isMeleeAttack = false;
        GetComponent<EnemyMovementSM>().eAnim.SetBool("punching", false);
        yield return new WaitForSeconds(GetComponent<EnemyMovementSM>().attackDelay);
        GetComponent<EnemyMovementSM>().isMeleeAttack = true;
        GetComponent<EnemyMovementSM>().eAnim.SetBool("punching", true);
    }

    IEnumerator Death()
    {
        GetComponent<EnemyMovementSM>().eAnim.SetBool("dead", true);
        yield return new WaitForSeconds(deadDuration);
        Destroy(enemy.gameObject);
    }
}
