using UnityEngine;

public class LiftButton : MonoBehaviour, IInteractable
{
    // What lift this button is connected to
    public Lift currentLift;
    public int targetFloor;

    public void Toggle()
    {
        // Tell the lift to go to the target floor
        currentLift.rMaster.buttonPressed = true;
        currentLift.rMaster.inLift = true;

        currentLift.GoToFloor(targetFloor);
    }

    public void OnInteract() { }

    public void OnLookAt() { }

    public void OnLookAway() { }
}
