using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float maxArmor; // For negating damage by 25%.
    public float armor; // The current armor amount.
    protected IDeathObserver deathObserver;

    protected virtual void Awake()
    {
        if (maxHealth <= 0) maxHealth = 100;
        health = maxHealth;

        if (maxArmor < 0) maxArmor = 100;
        armor = 0;
        deathObserver = GetComponent<IDeathObserver>();
    }

    public virtual void TakeDamage(float amount)
    {
        if (armor > 0)
        {
            float reduceDamage = amount * 0.75f; // Drop armor by the amount of damage dealt.

            // First, check if the armor is more than or equal to reduceDamage.
            // if it is, reduce armor by reduceDMG
            
            // However, if armor is not equal to it (i.e. it is breaking), then deal damage directly.
            // If no armor exists at ALL, deal damage directly to the user.

            if (armor >= reduceDamage)
            {
                armor -= reduceDamage;
            }
            else
            {
                float leftOverDMG = reduceDamage - armor; // Calculate left over damage.
                armor = 0;
                health -= leftOverDMG;
            }
        }
        else // No armor, deal damage at normal rate.
        {
            armor = 0; // Armor broke, deal regular damage now.
            health -= amount;
        }

        if (health <= 0)
        {
            health = 0;
            deathObserver?.OnDeath();
        }
    }

    // Step 1: Make a virtual void method called EquipArmor, which takes the variable float amount as the parameter
    public virtual void EquipArmor(float amount)
    {
        // Step 2: Cap the armor to 100, so it does not go over.
        armor = Mathf.Min(armor + amount, maxArmor);
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
