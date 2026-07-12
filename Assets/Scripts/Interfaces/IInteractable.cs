using UnityEngine;

public interface IInteractable
{
    bool isTriggered { get; set; }

    void Toggle();

    void OnLookAt();

    void OnLookAway();

    void OnInteract(GameObject interactedObj);
}
