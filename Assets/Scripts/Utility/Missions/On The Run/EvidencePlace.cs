using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EvidencePlace : MonoBehaviour
{
    public OnTheRun OTR;
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
        OTR.PlacedEvidence = true;
        OTR.missionComplete = true;
        OTR.canAccessWesteria = true;
        OTR.westeriaUnlocked.SetActive(true);
        OTR.objective.text = "";
        OTR.mission.text = "";
        OTR.objectiveHolder.SetActive(false);
        
    }
}
