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
    public static bool activateLevel;
    public int killedNPCS = 0;
    public float flashDelay = 0.5f;
    public int currentPoliceLevel;

    public bool spottedPlayer = false;
    float lastSighted = 0;
    const float pursuitAbort = 20f;

    private void Update()
    {
        AddingLevel();
        AbortPursuit();
    }

    public void AddingLevel()
    {
        if (!addingLevel && activateLevel)
        {
            activateLevel = false;
            addingLevel = true;
            policeBorder.SetActive(true);
            StartCoroutine(AddLevel());
        }
    }

    void RemovingLevel()
    {
        if (addingLevel && !activateLevel)
        {
            activateLevel = true;
            addingLevel = false;
            StartCoroutine(RemoveLevel());
        }
    }

    IEnumerator AddLevel()
    {
        levels[policeLevels - 1].SetActive(true);
        yield return new WaitForSeconds(flashDelay);
        levels[policeLevels - 1].SetActive(false);
        yield return new WaitForSeconds(flashDelay);
        levels[policeLevels - 1].SetActive(true);
    }

    IEnumerator RemoveLevel()
    {
        levels[policeLevels - 1].SetActive(false);
        yield return new WaitForSeconds(flashDelay);
        levels[policeLevels - 1].SetActive(true);
        yield return new WaitForSeconds(flashDelay);
        levels[policeLevels - 1].SetActive(false);
    }

    public void PlayerSpotted()
    {
        spottedPlayer = true;
        lastSighted = Time.time;
        levels[policeLevels - 1].SetActive(true);
    }

    public void AbortPursuit()
    {
        if (!spottedPlayer && Time.time - lastSighted > pursuitAbort)
        {
            policeLevels = 0;
            levels[policeLevels].SetActive(false);
        }
    }
}
