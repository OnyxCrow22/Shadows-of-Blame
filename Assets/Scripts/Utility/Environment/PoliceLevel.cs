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
    public int currentPolice;
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
            currentPolice = policeLevels;
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
        if (policeLevels > 0)
        {
            levels[currentPolice - 1].SetActive(true);
            yield return new WaitForSeconds(flashDelay);
            levels[currentPolice - 1].SetActive(false);
            yield return new WaitForSeconds(flashDelay);
            levels[currentPolice - 1].SetActive(true);
            StopCoroutine(AddLevel());
        }
        else
        {
            Debug.LogWarning("Warning! Arrays must start at 0!");
        }
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
        levels[policeLevels].SetActive(true);
    }

    public void UpdateLevel()
    {
        if (killedNPCS >= 1)
        {
            policeLevels = 1;
            activateLevel = true;
        }
        if (killedNPCS >= 3)
        {
            policeLevels = 2;
            AddingLevel();
        }
        if (killedNPCS >= 9)
        {
            policeLevels = 3;
            AddingLevel();
        }
        if (killedNPCS >= 12)
        {
            policeLevels = 4;
            AddingLevel();
        }
        if (killedNPCS >= 15)
        {
            policeLevels = 5;
            AddingLevel();
        }
    }

    public void AbortPursuit()
    {
        if (!spottedPlayer && Time.time - lastSighted > pursuitAbort)
        {
            policeLevels = 0;
            levels[policeLevels - 1].SetActive(false);
        }
    }
}
