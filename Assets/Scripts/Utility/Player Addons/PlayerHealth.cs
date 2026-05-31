using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int healthLoss;
    public int healthGain;
    public float protectedDuration;
    public int healthPerSecond;
    public Image healthBar;
    public Color defaultCol = new Color32(36, 72, 28, 255);
    public GameObject HUD;
    public GameObject missionFailed;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI FailedText;
    public bool Protected, isDead;
    public float healDelay = 5f;

    public bool takingDamage = false;
    public bool canRegen = false;

    public PlayerMovementSM playsm;
    public float deadDuration;
    public OnTheRun OTR;
    public WestralWoes WW;
    public RespawnPlayer respawn;

    private void Start()
    {
        maxHealth = health;
        isDead = false;
        Protected = false;
    }

    private void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 100);

        HealthText.text = "HP: " + health;

        if (health < 100 && !takingDamage)
        {
            StartCoroutine(PlayerRegen());
            canRegen = true;
        }
        else if (takingDamage)
        {
            canRegen = false;
        }
    }

    IEnumerator PlayerRegen()
    {
        if (canRegen && !takingDamage)
        {
            yield return new WaitForSeconds(healDelay);

            health += healthPerSecond;

            if (health > 20)
            {
                healthBar.color = defaultCol;
            }

            if (health >= maxHealth)
            {
                health = maxHealth;
                canRegen = false;
            }
        }
    }

    public void LoseHealth(int healthLoss)
    {
        health -= healthLoss;
        StartCoroutine(ProtectionTimer());


        if (health <= 20)
        {
            healthBar.color = Color.red;
        }

        if (health <= 0)
        {
            health = 0;
            respawn.CheckDeath();
            healthBar.enabled = false;
            CapsuleCollider playCol = GetComponent<CapsuleCollider>();
            playCol.enabled = false;
            isDead = true;
        }
    }

    IEnumerator ProtectionTimer()
    {
        Protected = true;
        yield return new WaitForSeconds(protectedDuration);
        Protected = false;
    }
}
