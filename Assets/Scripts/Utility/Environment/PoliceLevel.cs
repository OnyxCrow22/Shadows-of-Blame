using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLevel : MonoBehaviour
{
    public static PoliceLevel Instance; // Singleton access

    public GameObject[] levels;
    public GameObject policeBorder;

    public static int policeLevels = 0;
    public int killedNPCS = 0;
    public int killedOfficers = 0;

    private bool isUpdatingLevel = false; // Logic gate to stop UI flickering

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        // Only run logic if pursuit is active
        if (policeLevels > 0)
        {
            CheckThresholds();
        }
    }

    private void CheckThresholds()
    {
        int newLevel = 0;

        // Simplified threshold logic: higher counts override lower ones
        if (killedNPCS >= 15 || killedOfficers >= 7) newLevel = 5;
        else if (killedNPCS >= 12 || killedOfficers >= 5) newLevel = 4;
        else if (killedNPCS >= 9 || killedOfficers >= 3) newLevel = 3;
        else if (killedNPCS >= 3 || killedOfficers >= 1) newLevel = 2;
        else if (killedNPCS >= 1) newLevel = 1;

        if (newLevel > policeLevels)
        {
            StartCoroutine(UpdateLevelUI(newLevel));
        }
    }

    private IEnumerator UpdateLevelUI(int targetLevel)
    {
        if (isUpdatingLevel) yield break; // Prevent overlapping coroutines

        isUpdatingLevel = true;
        policeLevels = targetLevel;

        policeBorder.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        isUpdatingLevel = false;
    }

    public void AbortPursuit()
    {
        policeLevels = 0;
        killedNPCS = 0;
        killedOfficers = 0;
        policeBorder.SetActive(false);
        foreach (var level in levels) level.SetActive(false);
    }
}