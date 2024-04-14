using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidencePlace : MonoBehaviour
{
    public OnTheRun OTR;
    public bool EvidencePlaced;
    public GameObject blankEvidence;
    public GameObject filledEvidence;
    public AnimationClip fadeScreen;

    public void PlaceOnBoard()
    {
        StartCoroutine(EvidenceSwap());
    }

    public IEnumerator EvidenceSwap()
    {
        yield return new WaitForSeconds(3);
        blankEvidence.SetActive(false);
        filledEvidence.SetActive(true);
        OTR.PlacedEvidence = true;
        OTR.missionComplete = true;
        OTR.canAccessWesteria = true;
        OTR.westeriaUnlocked.SetActive(true);
    }
}
