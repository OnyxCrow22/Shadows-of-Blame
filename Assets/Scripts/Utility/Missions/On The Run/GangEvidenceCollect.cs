using UnityEngine;

public class GangEvidenceCollect : MonoBehaviour, IInteractable
{
    [Header("UI References")]
    public GameObject gEvidence;
    public GameObject gPanel;

    [Header("State")]
    public bool isgReading = false;
    public bool evidenceCollected = false;

    public bool isTriggered { get; set; }

    // Use the IInteractable signature required by your project
    public void OnInteract(GameObject interactor)
    {
        if (!isgReading)
        {
            GEPickup();
        }
    }

    private void GEPickup()
    {
        gPanel.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
        isgReading = true;
    }

    public void GECloseWindow()
    {
        // UI Cleanup
        gPanel.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        isgReading = false;

        // Logic Update
        evidenceCollected = true;
        gEvidence.SetActive(false);

        // Broadcast that the evidence was collected
        // The MissionManager will listen for this and update the UI accordingly
        MissionEvents.OnEvidenceCollected?.Invoke("GangEvidence");

        // Trigger the police chase via event
        MissionEvents.OnPoliceTriggered?.Invoke(true);
    }

    // Unused interface methods
    public void OnLookAt() { }
    public void OnLookAway() { }
    public void Toggle() { }
}