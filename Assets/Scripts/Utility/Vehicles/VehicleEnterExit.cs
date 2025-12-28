using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleEnterExit : MonoBehaviour
{
    [Header("Vehicle References")]
    private DefineVehicle currentVehicle;
    public bool canEnter = false;
    public bool canExit = false;
    public bool inVehicle = false;
    public PlayerMovementSM playsm;
    public GameObject player;
    public RaycastMaster rMaster;
    public PlayerInput pController;
    public float waitTime; // Time to wait for entering/exiting animations

    // Update is called once per frame
    private void Update()
    {
        bool promptShow = (canEnter && !inVehicle) || (canExit && inVehicle);
            rMaster.interactKey.SetActive(promptShow);
    }

    void OnTriggerEnter(Collider other)
    {
        DefineVehicle vehicle = other.GetComponentInParent<DefineVehicle>();
        if (vehicle != null)
        {
            canEnter = true;
            currentVehicle = vehicle;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<DefineVehicle>() != null)
        {
            canEnter = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (!inVehicle && canEnter)
        {
            StartCoroutine(EnteringVehicle());
            inVehicle = true;
            canExit = true;
        }
        else if (inVehicle && canExit)
        {
            StartCoroutine(ExitingVehicle());
            inVehicle = false;
            canExit = false;
        }
    }

    // This code will no longer use hard-coded wait times for entering/exiting vehicles.
    // Also, the player will always face the correct direction when entering the vehicle, not getting in backwards.
    // It will also remove the redundant disabling of components that have nothing to do with the vehicle.
    public IEnumerator EnteringVehicle()
    {
        currentVehicle.vehicleCollider.enabled = false;

        playsm.anim.SetBool("enteringCar", true);
        currentVehicle.doorAnimator.SetBool("doorOpen", true);
        AudioManager.manager.Play("CarDoor");

        yield return new WaitForSeconds(waitTime);

        // Switches the cameras
        currentVehicle.vehicleCamera.SetActive(true);
        rMaster.playerCamera.SetActive(false);
        rMaster.ThirdPersonCamera.SetActive(false);

        // Moves the player to the vehicle seat
        player.transform.position = currentVehicle.seat.position;
        player.transform.rotation = currentVehicle.seat.rotation;
        player.transform.parent = currentVehicle.transform;

        // End the animation
        playsm.anim.SetBool("enteringCar", false);
        currentVehicle.doorAnimator.SetBool("doorOpen", false);

        // Disable player controls and enable vehicle controls
        playsm.enabled = false;
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        // Enable vehicle control script
        currentVehicle.carController.enabled = true;

        inVehicle = true;
        playsm.inVehicle = true;
        canEnter = false;
        rMaster.interactKey.SetActive(false);
    }

    public IEnumerator ExitingVehicle()
    {
        currentVehicle.vehicleCollider.enabled = true;

        playsm.anim.SetBool("exitingCar", true);
        currentVehicle.doorAnimator.SetBool("doorOpen", true);
        AudioManager.manager.Play("CarDoor");

        yield return new WaitForSeconds(waitTime);

        // Switches the cameras
        currentVehicle.vehicleCamera.SetActive(false);
        rMaster.playerCamera.SetActive(true);
        rMaster.ThirdPersonCamera.SetActive(true);

        // Moves the player outside
        player.transform.parent = null;
        player.transform.position = currentVehicle.exitPoint.position;
        player.transform.rotation = currentVehicle.exitPoint.rotation;

        // End the animation
        playsm.anim.SetBool("exitingCar", false);
        currentVehicle.doorAnimator.SetBool("doorOpen", false);

        // enable player controls
        playsm.enabled = true;
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;

        // Disable vehicle control script
        currentVehicle.carController.enabled = false;

        inVehicle = false;
        playsm.inVehicle = false;
        canExit = false;
        rMaster.interactKey.SetActive(false);
    }
}

