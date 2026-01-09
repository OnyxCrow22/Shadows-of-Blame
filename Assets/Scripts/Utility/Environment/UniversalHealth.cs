using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UniversalHealth : MonoBehaviour
{

    public CharacterType characterType = CharacterType.Player;

    public float health;
    public float maxHealth;
    public float healDelay;
    public float healthPerSecond;

    public Image healthBar;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI FailedText;
    public bool isDead = false;
    public bool isCharacterProtected = false;

    public PlayerMovementSM playsm;

    public RespawnPlayer respawn;

    private float regenerationTimer;
    private bool takingDamage = false;

    public Color defaultCol = new Color32(36, 72, 28, 255);
    public Color dangerCol = Color.red;

    private bool canRegenerate => !takingDamage && health < maxHealth;
    private bool isInvincible => isDead || isCharacterProtected;

    private void Start()
    {
        maxHealth = health;
        isDead = false;
    }

    private void Update()
    {
        // Is the character dead?
        if (isDead) return;

        if (characterType == CharacterType.Player)
        {
            healthBar.fillAmount = Mathf.Clamp01(health / maxHealth);

            HealthText.text = $"HP: {Mathf.RoundToInt(health)}";
        }

        if (canRegenerate)
        {
            // Start the regeneration process
            regenerationTimer += Time.deltaTime;

            if (characterType == CharacterType.Player)
            {
                if (regenerationTimer >= healDelay)
                {
                    health = Mathf.MoveTowards(health, maxHealth, healthPerSecond * Time.deltaTime);

                    if (health > 20) healthBar.color = Color.Lerp(healthBar.color, defaultCol, health / maxHealth);
                }
            }
        }
    }

    public void LoseHealth(float damage)
    {
        // Is the character invincible?
        if (isInvincible) return;

        health -= damage; // Taking damage

        if (characterType == CharacterType.Player)
        {
            regenerationTimer = 0; // No longer able to regenerate
            StartCoroutine(ProtectionTimer());
            if (health <= 20) healthBar.color = Color.red;
        }

        if (health <= 0)
        {
            HandleDeath();
        }
    }

    public void HandleDeath()
    {
        health = 0;
        isDead = true;

        if (respawn != null)
        {
            respawn.CheckDeath();
        }

        if (characterType == CharacterType.Player)
        {
            healthBar.enabled = false;
            playsm.anim.SetBool("isDead", true);

            if (TryGetComponent<CapsuleCollider>(out var col)) col.enabled = false;
            playsm.enabled = false;
        }
        else
        {
            Destroy(gameObject, 5f);
        }
    }

    IEnumerator ProtectionTimer()
    {
        isCharacterProtected = true;
        yield return new WaitForSeconds(regenerationTimer);
        isCharacterProtected = false;
    }
}
