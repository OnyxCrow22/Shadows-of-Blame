using System.Collections;
using System.Collections.Generic;
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
    public GameObject HUD;
    public Transform respawnPoint;
    public bool Protected, isDead;

    public PlayerMovementSM playsm;
    public EnemyMovementSM esm;

    private void Start()
    {
        maxHealth = health;
        isDead = false;
        Protected = false;
        esm = GetComponent<EnemyMovementSM>();
    }

    private void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 100);

        if (!esm.isAttacking && health < 100)
        {
            healthGain += healthPerSecond * Time.deltaTime;

            if (healthGain == maxHealth)
            {
                healthGain = 100;
            }
        }
    }

    public void LoseHealth(float healthLoss)
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
        playsm.gameObject.SetActive(false);
        CapsuleCollider playCol = GetComponent<CapsuleCollider>();
        playCol.direction = 2;
        playsm.anim.SetBool("dead", true);
        isDead = true;
        yield return new WaitForSeconds(deadDuration);
        isDead = false;
        playsm.anim.SetBool("dead", false);
        playsm.player.transform.position = respawnPoint.transform.position;
        Physics.SyncTransforms();
        playsm.gameObject.SetActive(true);
        healthBar.color = Color.clear;
        health = 100;
        maxHealth = 100;
        healthBar.enabled = true;
    }
}
