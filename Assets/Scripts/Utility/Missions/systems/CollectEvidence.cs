using UnityEngine;

// Define a static class for game-wide events
public static class GameEvents
{
    // Passes the ID of the evidence collected
    public static System.Action<string> OnEvidenceCollected;
}

public class CollectEvidence : MonoBehaviour, IInteractable
{
    public bool isTriggered { get; set; }

    [Header("Data")]
    public string evidenceID;

    [Header("References")]
    public GameObject visualEvidence;

    public void OnInteract(GameObject interactor)
    {
        // Broadcast the event to anyone listening (Mission Manager, Sound Manager, etc.)
        GameEvents.OnEvidenceCollected?.Invoke(evidenceID);

        if (visualEvidence != null)
            visualEvidence.SetActive(false);
    }

    // Interaction prompts are now handled by the RaycastMaster itself 
    // by detecting the IInteractable interface
    public void OnLookAt() { /* RaycastMaster handles this */ }
    public void OnLookAway() { /* RaycastMaster handles this */ }
    public void Toggle() { }
}