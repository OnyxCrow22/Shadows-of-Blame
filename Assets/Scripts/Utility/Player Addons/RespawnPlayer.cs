using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RespawnPlayer : MonoBehaviour
{
    public PlayerMovementSM playsm;
    public GameObject missionFailed;
    public GameObject HUD;
    public TextMeshProUGUI FailedText;
    public PlayerHealth pHealth;
    public WestralWoes WW;
    public OnTheRun OTR;
    public GameObject[] respawnPoints;
    public void CheckDeath()
    {
        if (pHealth.health == 0)
        {
            StartCoroutine(Respawning());
        }
    }
    public IEnumerator Respawning()
    {
        playsm.anim.SetBool("dead", true);
        missionFailed.SetActive(true);
        HUD.SetActive(false);
        FailedText.text = "Harrison died!";
        Debug.Log("DEAD!");
        yield return new WaitForSeconds(pHealth.deadDuration);
        pHealth.isDead = false;
        missionFailed.SetActive(false);
        HUD.SetActive(true);
        playsm.anim.SetBool("dead", false);
        pHealth.healthBar.color = new Color32(36, 72, 28, 255);
        pHealth.health = 100;
        pHealth.maxHealth = 100;
        pHealth.healthBar.enabled = true;

        if (OTR.westeriaUnlocked || WW.onWestInsbury)
        {
            int RandomSpawnSelect = Random.Range(0, respawnPoints.Length);

            // Spawn at either Halifax Park General Hospital or Saint Mary's Hospital.
            playsm.player.transform.position = respawnPoints[RandomSpawnSelect].transform.position;
            Physics.SyncTransforms();
        }
        else if (OTR.westeriaUnlocked == false || WW.onWestInsbury == false)
        {
            // Respawn the player at Saint Mary's Hospital.
            playsm.player.transform.position = respawnPoints[0].transform.position;
            Physics.SyncTransforms();
            HUD.SetActive(true);
            missionFailed.SetActive(false);
        }
    }
}
