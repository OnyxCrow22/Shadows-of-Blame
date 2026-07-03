using UnityEngine;
using UnityEngine.Events;

public class CollectEvidence : MonoBehaviour, IInteractable
{
    [Header("Data")]
    public string evidenceID; // Unique evidence tag
    public string clueText; // Unique clue text for each individual clue

    [Header("References")]
    public GameObject visualEvidence; // The evidence object itself;
    public RaycastMaster rMaster; // Referencing to the Raycast Master

    [Header("Events")]
    public UnityEvent<string, string> OnEvidenceCollected; // Use the evidenceID and clueText, and subscribe to the Unity Event system.

    public void OnInteract()
    {
        // Hide the evidence panel and set isReading to false
        OnEvidenceCollected?.Invoke(evidenceID, clueText); // Fire the event
        if (visualEvidence != null) visualEvidence.SetActive(false); // Turn off the gameObject
        if (rMaster?.interactKey != null) rMaster.interactKey.SetActive(false); // Turn off the prompt to read it
    }

    public void OnLookAt()
    {
        if (rMaster?.interactKey != null) // Not currently reading?
            rMaster.interactKey.SetActive(true); // Turn on the prompt to read it
    }

    public void OnLookAway()
    {
        if (rMaster?.interactKey != null)
            rMaster.interactKey.SetActive(false);
    }

    public void Toggle() { }
}
