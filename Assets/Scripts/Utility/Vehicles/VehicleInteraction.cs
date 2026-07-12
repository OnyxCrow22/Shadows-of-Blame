using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleInteraction : MonoBehaviour, IInteractable
{
    [Header("References")]
    public VehicleEntrySystem entrySystem;
    public VehicleDefinition vehicle;
    public bool isTriggered { get; set; }
    public VehicleState vehicleState;

    public GameObject interactPrompt;   // UI prompt (Press E)
    public PlayerInput playerInput;

    private bool lookingAtVehicle = false;

    private void Awake()
    {
        interactPrompt.SetActive(false);
    }

    // Called by your RaycastMaster when the player looks at the vehicle
    public void OnLookAt()
    {
        lookingAtVehicle = true;

        if (!vehicleState.IsPlayerInside)
            interactPrompt.SetActive(true);
    }

    // Called when the player looks away
    public void OnLookAway()
    {
        lookingAtVehicle = false;
        interactPrompt.SetActive(false);
    }

    // Called when the player presses the interact key
    public void OnInteract(GameObject interactor)
    {
        if (!lookingAtVehicle)
            return;

        if (!vehicleState.IsPlayerInside)
        {
            // Enter vehicle
            interactPrompt.SetActive(false);
            entrySystem.EnterVehicle();
        }
        else
        {
            // Exit vehicle
            entrySystem.ExitVehicle();
        }
    }

    public void Toggle() { }
}
