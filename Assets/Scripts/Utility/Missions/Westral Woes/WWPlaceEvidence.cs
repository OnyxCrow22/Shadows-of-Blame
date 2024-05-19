using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WWPlaceEvidence : MonoBehaviour
{
    public WestralWoes WW;
    public EndCredits ending;
    public bool EvidencePlaced;
    public GameObject blankEvidence;
    public GameObject filledEvidence;
    public Animator fadeScreen;

    public IEnumerator EvidenceSwap()
    {
        yield return new WaitForSeconds(3);
        fadeScreen.GetComponent<Animator>().enabled = true;
        fadeScreen.SetBool("fading", true);
        blankEvidence.SetActive(false);
        filledEvidence.SetActive(true);
        fadeScreen.SetBool("fading", false);
        fadeScreen.GetComponent<Animator>().enabled = false;
        WW.placedEvidence = true;
        WW.missionComplete = true;
        WW.objective.text = "";
        WW.WWHolder.SetActive(false);
        WW.enabled = false;
        StartCoroutine(ending.Credits());
    }
}
