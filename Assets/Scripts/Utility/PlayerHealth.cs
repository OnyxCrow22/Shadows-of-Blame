using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthLoss;
    public float protectedDuration;
    public float deadDuration;
    public Image healthBar;
    public GameObject HUD;
    private PlayerMovementSM playsm;
    public GameObject[] respawnPoints;
    public bool Protected;

    private void Start()
    {
        maxHealth = health;
    }

    private void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 100);
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
            Dead();
        }
    }

    IEnumerator ProtectionTimer()
    {
        Protected = true;
        yield return new WaitForSeconds(protectedDuration);
        Protected = false;
    }

    public void Dead()
    {
        playsm.anim.SetBool("dead", true);
        Time.timeScale = 0.5f;
        Invoke("Dead", deadDuration);
        int RandomIndex = Random.Range(0, respawnPoints.Length);

        if (respawnPoints.Length == 0)
        {
            Time.timeScale = 1;
            playsm.transform.position = respawnPoints[0].transform.position;
            playsm.anim.SetBool("dead", false);
        }
    }
}
