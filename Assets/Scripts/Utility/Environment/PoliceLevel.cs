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
    public int killedNPCS;
    public int killedOfficers;
    public float flashDelay = 0.5f;

    public GangEvidenceCollect alejandro;
    public WWNorthBeachEvidence NorthBeach;
    public PoliceEvaded evaded;
    public OnTheRun OTR;

    public bool spottedPlayer = false;
    public bool cancelPursuit = false;
    float lastSighted = 0;

    private void Update()
    {
        if (!cancelPursuit)
        {
            AddingLevel();
            UpdateLevel();
        }
    }

    public void CheckPursuit()
    {
        if (cancelPursuit == true && activateLevel)
        {
            cancelPursuit = false;
        }
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
        if (!spottedPlayer && policeLevels >= 1 && !cancelPursuit)
        {
            StartCoroutine(PlayerSearch());
            Debug.Log("Begin search...");
        }
        else if (cancelPursuit)
        {
            AbortPursuit();
        }
    }

    public IEnumerator PlayerSearch()
    {
        float searchTime = 0;

        while (searchTime < 5)
        {
            yield return new WaitForSeconds(1);
            searchTime++;
            if (!spottedPlayer && searchTime >= 5)
            {
                AbortPursuit();
                yield break;
            }
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
        if (killedNPCS == 3 || killedOfficers == 1)
        {
            policeLevels = 2;
            activateLevel = true;
        }
        if (killedNPCS == 9 || killedOfficers == 3)
        {
            policeLevels = 3;
            activateLevel = true;
        }
        if (killedNPCS == 12 || killedOfficers == 5)
        {
            policeLevels = 4;
            activateLevel = true;
        }
        if (killedNPCS == 15 || killedOfficers == 7)
        {
            policeLevels = 5;
            activateLevel = true;
        }

        if (OTR.wLocked.attemptingWesteria)
        {
            policeLevels = 5;
            activateLevel = true;
        }

        if (alejandro.evidence)
        {
            policeLevels = 1;
            activateLevel = true;
        }

        if (NorthBeach.evidence)
        {
            policeLevels = 1;
            activateLevel = true;
        }

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i < policeLevels);
        }
    }

    public void AbortPursuit()
    {
        activateLevel = false;
        addingLevel = false;
        policeBorder.SetActive(false);
        cancelPursuit = true;
        policeLevels = 0;
        killedNPCS = 0;
        Debug.Log("All units: Harrison Felton has escaped. Return to patrols. Repeat: Return to patrols, Felton has escaped. Don't tell the police commissioner..");

        if (alejandro.evidence)
        {
            alejandro.CancelPursuit();
        }
        if (NorthBeach.evidence)
        {
            NorthBeach.Next();
        }

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }
}
