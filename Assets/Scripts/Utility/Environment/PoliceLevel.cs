using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceLevel : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject policeBorder;
    public bool addingLevel;
    public static int policeLevels;
    public int currentPolice = 0;
    public static bool activateLevel;
    public int killedNPCS;
    public float flashDelay = 0.5f;

    public bool spottedPlayer = false;
    float lastSighted = 0;
    const float pursuitAbort = 20f;

    private void Update()
    {
        AddingLevel();
        UpdateLevel();
    }

    public void AddingLevel()
    {
        if (!addingLevel && activateLevel)
        {
            activateLevel = false;
            addingLevel = true;
            policeBorder.SetActive(true);
            currentPolice = policeLevels;
            StartCoroutine(AddLevel());
        }
    }

    IEnumerator AddLevel()
    {
        for (int i = 0; i < policeLevels; i++)
        {
            levels[i].SetActive(true);
            yield return new WaitForSeconds(flashDelay);
            levels[i].SetActive(false);
            yield return new WaitForSeconds(flashDelay);
            levels[i].SetActive(true);
        }
    }

    public void LostPlayer()
    {
        if (!spottedPlayer && policeLevels >= 1)
        {
            StartCoroutine(PlayerSearch());
        }
    }

    public IEnumerator PlayerSearch()
    {
        if (!spottedPlayer)
        {
            AbortPursuit();
            yield break;
        }
    }

    public void PlayerSpotted()
    {
        spottedPlayer = true;
        lastSighted = Time.time;
        levels[policeLevels].SetActive(true);
    }

    public void UpdateLevel()
    {
        if (killedNPCS == 1)
        {
            policeLevels = 1;
            activateLevel = true;
        }
        if (killedNPCS == 3)
        {
            policeLevels = 2;
            activateLevel = true;
        }
        if (killedNPCS == 9)
        {
            policeLevels = 3;
            activateLevel = true;
        }
        if (killedNPCS == 12)
        {
            policeLevels = 4;
            activateLevel = true;
        }
        if (killedNPCS == 15)
        {
            policeLevels = 5;
            activateLevel = true;
        }

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i < policeLevels);
        }
    }

    public void AbortPursuit()
    {
        policeLevels = 0;
        for (int i = 0; i < levels.Length;i++)
        {
            levels[i].SetActive(false);
            activateLevel = false;
            addingLevel = false;
            policeBorder.SetActive(false);
        }
    }
}
