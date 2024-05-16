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
    public TextMeshProUGUI HealthText;
    public bool Protected, isDead;
    public float healDelay = 5f;

    public bool takingDamage = false;
    public bool canRegen = false;

    public PlayerMovementSM playsm;
    public float deadDuration;
    public GameObject[] respawnPoints;
    public OnTheRun OTR;
    public WestralWoes WW;

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

            if (health >= 100)
            {
                health = 100;
                maxHealth = 100;
            }
        }
        else if (takingDamage)
        {
            canRegen = false;
            StopCoroutine(PlayerRegen());
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
            maxHealth = 0;
            healthBar.enabled = false;
            StartCoroutine(Respawning());
            isDead = true;
        }
    }
    public IEnumerator Respawning()
    {
        CapsuleCollider playCol = GetComponent<CapsuleCollider>();
        playCol.direction = 2;
        playsm.anim.SetBool("dead", true);
        yield return new WaitForSeconds(deadDuration);
        isDead = false;
        playsm.anim.SetBool("dead", false);
        healthBar.color = new Color32(36, 72, 28, 255);
        health = 100;
        maxHealth = 100;
        healthBar.enabled = true;

        if (OTR.westeriaUnlocked == true || WW.onWestInsbury == true)
        {
            int RandomSpawnSelect = Random.Range(0, respawnPoints.Length);

            // Spawn at either Halifax Park General Hospital or Saint Mary's Hospital.
            playsm.player.transform.position = respawnPoints[RandomSpawnSelect].transform.position;
            Physics.SyncTransforms();
        }
        else if (!OTR.westeriaUnlocked || !WW.onWestInsbury == false)
        {
            // Respawn the player at Saint Mary's Hospital.
            playsm.player.transform.position = respawnPoints[0].transform.position;
            Physics.SyncTransforms();
        }
    }

    IEnumerator ProtectionTimer()
    {
        Protected = true;
        yield return new WaitForSeconds(protectedDuration);
        Protected = false;
    }
}
