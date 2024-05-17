using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WesteriaLocked : MonoBehaviour
{
    public OnTheRun OTR;
    public WesteriaAccessibility access;
    public bool attemptingWesteria;
    public bool pushedBack = false;
    public PoliceLevel pLevel;
    public GameObject player;
    public GameObject pushBack;
    public GameObject MainUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !OTR.canAccessWesteria && access.warning || other.CompareTag("Vehicle") && !OTR.canAccessWesteria && access.warning)
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
        yield return new WaitForSecondsRealtime(5);
        OTR.dangerPanel.SetActive(false);
        PoliceLevel.policeLevels = 5;
        PoliceLevel.activateLevel = true;
        OTR.missionFailed.SetActive(true);
        MainUI.SetActive(false);
        AudioListener.pause = true;
        OTR.failText.text = "Harrison attracted attention to himself.";
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(10);
        player.transform.position = pushBack.transform.position;
        pushedBack = true;
        pLevel.AbortPursuit();
        Physics.SyncTransforms();
        OTR.missionFailed.SetActive(false);
        Time.timeScale = 1;
        MainUI.SetActive(true);
        AudioListener.pause = false;
    }
}
