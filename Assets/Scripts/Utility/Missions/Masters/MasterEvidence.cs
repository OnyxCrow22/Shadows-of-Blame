using UnityEngine;

public class MasterEvidence : MonoBehaviour, IInteractable
{
    public enum missionType
    {
        OnTheRun,
        WestralWoes,
        None
    }

    [Header("Mission Type")]
    // The current mission type for this evidence master.
    public missionType currentMission;
    public string locationID;

    [Header("UI References")]
    public GameObject readingPanel;
    public GameObject clueText;

    [Header("Manager references")]
    public OnTheRun OTR;
    public WestralWoes WW;
    public RaycastMaster rMaster;

    private bool isReading = false;

    public void OnLookAt()
    {
        if (!isReading)
        {
            rMaster.interactKey.SetActive(true);
        }
    }

    public void OnLookAway() { rMaster.interactKey.SetActive(false); }

    public void Toggle()
    {
        if (!isReading) PickUp();
        else CloseWindow();
    }

    public void OnInteract() {  }

    public void PickUp()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        readingPanel.SetActive(true);
        isReading = true;
        rMaster.interactKey.SetActive(false);
    }

    public void CloseWindow()
    {
        isReading = false;
        readingPanel.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;

        UpdateMissionProgress();

        this.gameObject.SetActive(false);
    }

    public void UpdateMissionProgress()
    {
        switch (currentMission)
        {
            case missionType.OnTheRun:
                OTR.collectedEvidence += 1;
                clueText.SetActive(true);
                OTR.clue.SetActive(true);
                OTR.objective.text = "Search Westral Square for evidence: " + OTR.collectedEvidence + " / " + OTR.totalEvidence;
                if (OTR.collectedEvidence == OTR.totalEvidence)
                {
                    OTR.Evidence = true;
                    OTR.clue.SetActive(false);
                    OTR.magGlass.SetActive(false);
                    OTR.objective.text = "Go to the gang compound.";
                }
                break;
            case missionType.WestralWoes:
                {
                    // Westral Woes evidence collection logic
                }
                break;
            case missionType.None:
                Debug.LogWarning("No mission type assigned to MasterEvidence.");
                break;
        }
    }
}
