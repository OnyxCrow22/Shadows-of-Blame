using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
    void Die();
}

public interface IDeathObserver
{
    void OnDeath();
}
