using System.Collections;
using UnityEngine;

public class UniversalObjectiveTrigger : MonoBehaviour, IInteractable
{
    [Header("Configuration")]
    public string objectiveID;

    public bool isTriggered { get; set; }

    [Header("Visuals")]
    public GameObject beforeState;
    public GameObject afterState;

    [Header("Optional")]
    public Animator fadeScreen; // Now you can assign an animator if needed

    // Satisfy the interface
    public void OnInteract(GameObject interactor)
    {
        StartCoroutine(ActivateSequence());
    }

    private IEnumerator ActivateSequence()
    {
        // Handle visual swap
        if (beforeState) beforeState.SetActive(false);
        if (afterState) afterState.SetActive(true);

        // Broadcast the completion
        MissionEvents.OnObjectiveComplete?.Invoke(objectiveID);

        yield break;
    }

    // Empty interface requirements
    public void OnLookAt() { }
    public void OnLookAway() { }
    public void Toggle() { }
}