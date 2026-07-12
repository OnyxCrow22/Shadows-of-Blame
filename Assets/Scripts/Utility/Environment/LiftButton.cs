using UnityEngine;

public class LiftButton : MonoBehaviour, IInteractable
{
    public Lift currentLift;
    public int targetFloor;
    public bool isTriggered { get; set; }

    public void OnInteract(GameObject user) => currentLift.MoveToFloor(targetFloor);
    public void Toggle() => currentLift.MoveToFloor(targetFloor);
    public void OnLookAt() { }
    public void OnLookAway() { }
}