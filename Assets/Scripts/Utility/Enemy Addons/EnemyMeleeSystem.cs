using UnityEngine;

public class EnemyMeleeSystem : MonoBehaviour
{
    public void PerformAttack(float damage, HealthSystem targetHealth)
    {
        targetHealth.TakeDamage(damage);
        Debug.Log("Attack landed!");
    }
}