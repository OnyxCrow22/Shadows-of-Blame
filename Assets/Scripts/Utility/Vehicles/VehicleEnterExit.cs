using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class VehicleEntrySystem : MonoBehaviour
{
    public VehicleDefinition vehicle;
    public VehicleState vehicleState;

    public GameObject player;
    public PlayerInput playerInput;

    public Animator playerAnimator;
    public Camera playerCamera;
    public Camera vehicleCamera;

    public float animationWait = 1f;

    private bool inVehicle = false;

    public void EnterVehicle()
    {
        if (inVehicle) return;
        StartCoroutine(EnterRoutine());
    }

    public void ExitVehicle()
    {
        if (!inVehicle) return;
        StartCoroutine(ExitRoutine());
    }

    private IEnumerator EnterRoutine()
    {
        inVehicle = true;
        vehicleState.SetPlayerInside(true);

        // Play animations
        vehicle.doorAnimator.SetBool("doorOpen", true);
        playerAnimator.SetBool("enteringCar", true);

        yield return new WaitForSeconds(animationWait);

        // Move player to seat
        player.transform.position = vehicle.seat.position;
        player.transform.rotation = vehicle.seat.rotation;

        // Switch cameras
        playerCamera.gameObject.SetActive(false);
        vehicleCamera.gameObject.SetActive(true);

        // Disable player movement
        playerInput.enabled = false;

        // Enable vehicle controls
        vehicle.physicsController.enabled = true;
        vehicle.uiController.enabled = true;

        // End animations
        vehicle.doorAnimator.SetBool("doorOpen", false);
        playerAnimator.SetBool("enteringCar", false);
    }

    private IEnumerator ExitRoutine()
    {
        vehicle.doorAnimator.SetBool("doorOpen", true);
        playerAnimator.SetBool("exitingCar", true);

        yield return new WaitForSeconds(animationWait);

        // Move player outside
        player.transform.position = vehicle.exitPoint.position;
        player.transform.rotation = vehicle.exitPoint.rotation;

        // Switch cameras
        vehicleCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);

        // Enable player movement
        playerInput.enabled = true;

        // Disable vehicle controls
        vehicle.physicsController.enabled = false;
        vehicle.uiController.enabled = false;

        inVehicle = false;
        vehicleState.SetPlayerInside(false);

        // End animations
        vehicle.doorAnimator.SetBool("doorOpen", false);
        playerAnimator.SetBool("exitingCar", false);
    }
}
