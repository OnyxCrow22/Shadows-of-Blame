using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftCall : MonoBehaviour, IInteractable
{
    public Lift liftToCall;
    public int floorButton;

    public void OnInteract()
    {

    }

    public void OnLookAt()
    {

    }

    public void Toggle()
    {
        liftToCall.rMaster.buttonPressed = true;
        liftToCall.rMaster.inLift = true;

        liftToCall.GoToFloor(floorButton);
    }

    public void OnLookAway()
    {

    }
}
