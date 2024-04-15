using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WesteriaLocked : MonoBehaviour
{
    public OnTheRun OTR;
    public WesteriaAccessibility access;
    public bool attemptingWesteria;
    public PoliceLevel pLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !OTR.canAccessWesteria && access.warning)
        {
            StartCoroutine(WarnedPlayer());
            access.warning = false;
            OTR.warningText.text = "";
            OTR.warningHolder.SetActive(false);
        }
    }

    public IEnumerator WarnedPlayer()
    {
        OTR.dangerPanel.SetActive(true);
        OTR.dangerText.text = "YOU WAS WARNED! YOU WILL NOW FACE THE WRATH OF THE WESTRAL POLICE!!";
        yield return new WaitForSeconds(5);
        OTR.dangerPanel.SetActive(false);
        PoliceLevel.levelStage += 5;
        PoliceLevel.giveLevel = true;
    }
}
