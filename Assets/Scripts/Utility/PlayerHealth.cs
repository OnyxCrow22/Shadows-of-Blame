using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    public float healthLoss = 10;
    public float protectedDuration;
    public float deadDuration;
    public Image healthBar;
    public GameObject HUD;
    private PlayerMovementSM playsm;
    public Transform respawnPoint;
    public bool Protected, isDead;

    private void Start()
    {
        maxHealth = health;
        isDead = false;
        Protected = false;
    }

    private void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 100);
    }

    public void LoseHealth()
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
        isDead = true;
        yield return new WaitForSeconds(deadDuration);
        isDead = false;
        playsm.har.transform.position = respawnPoint.transform.position;
    }
}
