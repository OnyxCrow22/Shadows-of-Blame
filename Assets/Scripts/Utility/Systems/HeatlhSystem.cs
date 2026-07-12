using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    public float health;
    public float maxHealth;
    protected IDeathObserver deathObserver;

    protected virtual void Awake()
    {
        health = maxHealth;
        deathObserver = GetComponent<IDeathObserver>();
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            deathObserver?.OnDeath();
        }
    }

    public virtual void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
    }

    public virtual void ResetHealth()
    {
        health = maxHealth;
    }
}
