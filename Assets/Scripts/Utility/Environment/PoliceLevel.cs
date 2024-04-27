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

    private void Update()
    {
        AddingLevel();
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
}
