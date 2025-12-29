using System.Collections;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleEnterExit : MonoBehaviour, IInteractable
{
    [Header("Vehicle References")]
    private DefineVehicle currentVehicle;
    public PlayerMovementSM playsm;
    public GameObject player;
    public RaycastMaster rMaster;
    public bool inVehicle = false;
    public float waitTime; // Time to wait for entering/exiting animations

    public void OnLookAt()
    {
        if (!inVehicle) 
            rMaster.interactKey.SetActive(true);
    }

    public void OnInteract() { }

    public void OnLookAway()
    {
        rMaster.interactKey.SetActive(false);
    }

    public void Toggle()
    {
        if (!inVehicle)
        {
            StartCoroutine(EnteringVehicle());
        }
        else
        {
            StartCoroutine(ExitingVehicle());
        }
    }

    // This code will no longer use hard-coded wait times for entering/exiting vehicles.
    // Also, the player will always face the correct direction when entering the vehicle, not getting in backwards.
    // It will also remove the redundant disabling of components that have nothing to do with the vehicle.
    public IEnumerator EnteringVehicle()
    {
        inVehicle = true;
        rMaster.interactKey.SetActive(false);

        currentVehicle.vehicleCollider.enabled = false;
        playsm.anim.SetBool("enteringCar", true);
        currentVehicle.doorAnimator.SetBool("doorOpen", true);
        AudioManager.manager.Play("CarDoor");

        yield return new WaitForSeconds(waitTime);

        // Switches the cameras
        currentVehicle.vehicleCamera.SetActive(true);
        rMaster.playerCamera.SetActive(false);

        // Moves the player to the vehicle seat
        player.transform.position = currentVehicle.seat.position;
        player.transform.rotation = currentVehicle.seat.rotation;
        player.transform.SetParent(currentVehicle.transform);

        // Disable player collider and character controller
        playsm.enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        // Disable player controls and enable vehicle controls
        currentVehicle.carController.enabled = true;
        playsm.inVehicle = true;

        // End the animation
        playsm.anim.SetBool("enteringCar", false);
        currentVehicle.doorAnimator.SetBool("doorOpen", false);
    }

    public IEnumerator ExitingVehicle()
    {
        playsm.anim.SetBool("exitingCar", true);
        currentVehicle.doorAnimator.SetBool("doorOpen", true);
        AudioManager.manager.Play("CarDoor");

        yield return new WaitForSeconds(waitTime);

        // Moves the player outside
        player.transform.SetParent(null);
        player.transform.position = currentVehicle.exitPoint.position;
        player.transform.rotation = currentVehicle.exitPoint.rotation;

        // Switches the cameras
        currentVehicle.vehicleCamera.SetActive(false);
        rMaster.playerCamera.SetActive(true);

        // enable player controls
        playsm.enabled = true;
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;

        // End the animation
        // Disable vehicle control script
        currentVehicle.carController.enabled = false;
        inVehicle = false;
        playsm.inVehicle = false;

        playsm.anim.SetBool("exitingCar", false);
        currentVehicle.doorAnimator.SetBool("doorOpen", false);
    }
}

