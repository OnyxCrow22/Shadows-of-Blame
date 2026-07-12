using UnityEngine;

public class LiftCall : MonoBehaviour, IInteractable
{
    public Lift liftToCall;
    public int floorButton;
    // Missing required property[cite: 12]
    public bool isTriggered { get; set; }

    public void OnInteract(GameObject user) // Missing required parameter[cite: 12]
    {
        Toggle();
    }

    public void Toggle()
    {
        // Updated to match the new method name in Lift.cs
        liftToCall.MoveToFloor(floorButton);
    }

    public void OnLookAt() { }
    public void OnLookAway() { }
}