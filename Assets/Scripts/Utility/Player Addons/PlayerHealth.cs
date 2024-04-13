using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthLoss;
    public float healthGain;
    public float protectedDuration;
    public float healthPerSecond;
    public float deadDuration;
    public Image healthBar;
    public Color defaultCol = new Color32(36, 72, 28, 255);
    public GameObject HUD;
    public Transform respawnPoint;
    public TextMeshProUGUI HealthText;
    public bool Protected, isDead, hasBeenAttacked = false;
    public float healDelay = 1.5f;

    public PlayerMovementSM playsm;

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

        if (health < 100 && !hasBeenAttacked)
        {
            StartCoroutine(PlayerRegen());
        }
    }

    IEnumerator PlayerRegen()
    {
        yield return new WaitForSeconds(healDelay);

        health += healthPerSecond * Time.deltaTime;

        if (health > 20)
        {
            healthBar.color = defaultCol;
        }

        if (health == 100)
        {
            health = 100;
        }
        else if (health< 100 && hasBeenAttacked)
        {
            healthGain = health;
        }

    }

    public void LoseHealth(float healthLoss)
    {
        health -= healthLoss;
        HealthText.text = "HP: " + health;
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
            StartCoroutine(Dead());
        }
    }

    IEnumerator ProtectionTimer()
    {
        Protected = true;
        yield return new WaitForSeconds(protectedDuration);
        Protected = false;
    }

    IEnumerator Dead()
    {
        CapsuleCollider playCol = GetComponent<CapsuleCollider>();
        playCol.direction = 2;
        playsm.anim.SetBool("dead", true);
        isDead = true;
        yield return new WaitForSeconds(deadDuration);
        isDead = false;
        playsm.anim.SetBool("dead", false);
        playsm.player.transform.position = respawnPoint.transform.position;
        Physics.SyncTransforms();
        healthBar.color = new Color32(36, 72, 28, 255);
        health = 100;
        maxHealth = 100;
        healthBar.enabled = true;
    }
}
