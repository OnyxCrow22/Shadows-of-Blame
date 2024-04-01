using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeSystem : MonoBehaviour
{
    public EnemyMovementSM esm;

    public void AttackPlayer()
    {
        esm.health.LoseHealth(esm.health.healthLoss);
        Debug.Log($"You was hit by {esm.enemy}");
        esm.attackedPlayer = true;

        if (esm.attackedPlayer)
        {
            ResetAttack();
            esm.isMeleeAttack = false;
            esm.isDealDamage = false;
            esm.attackedPlayer = false;
        }
    }

    public void ResetAttack()
    {
        StartCoroutine(ResetPunch());
    }

    IEnumerator ResetPunch()
    {
        yield return new WaitForSeconds(esm.attackDelay);
        esm.eAnim.SetBool("punching", false);
        yield return new WaitForSeconds(esm.attackDelay);
        esm.isMeleeAttack = true;
        esm.isDealDamage = true;
        esm.eAnim.SetBool("punching", true);
    }
}
