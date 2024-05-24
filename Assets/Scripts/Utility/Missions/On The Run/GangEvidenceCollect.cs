using UnityEngine;

public class GangEvidenceCollect : MonoBehaviour
{
    public GameObject gEvidence;
    public GameObject gPanel;
    public GameObject coWorker;
    public bool isgReading = false;
    public bool evidence = false;
    public bool Escaped = false;
    public OnTheRun OTR;
    public RaycastMaster rMaster;
    public PoliceLevel police;
    public PoliceEvaded policeCheck;

    public void Start()
    {
        OTR.Escaped = false;
        Escaped = false;
    }
    public void GEPickup()
    {
        gPanel.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void GECloseWindow()
    {
        gPanel.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        isgReading = false;
        rMaster.interactKey.SetActive(false);
        evidence = true;
        OTR.GangEvidence = true;
        gEvidence.SetActive(false);
        OTR.police.cancelPursuit = false;
        police.UpdateLevel();
        PoliceLevel.activateLevel = true;
        OTR.objective.text = "Lose the police.";

        Debug.Log($"Cancel the pursuit, Felton is gone: {OTR.police.cancelPursuit}");
    }

    public void CancelPursuit()
    {
        Debug.Log($"SUCCESSFULLY EVADED THE POLICE");
        OTR.objective.text = "Go to your safehouse.";
        OTR.Escaped = true;
        PoliceLevel.activateLevel = false;
        Escaped = true;
    }
}