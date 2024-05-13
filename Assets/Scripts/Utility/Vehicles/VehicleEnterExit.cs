using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VehicleEnterExit : MonoBehaviour
{
    [Header("Vehicle References")]
    public GameObject vehicleCam;
    public GameObject playerCam;
    public GameObject TPCam;
    public GameObject player;
    public GameObject vehicle;
    public GameObject exitPoint;
    public Transform carSeat;
    public bool canEnter = false;
    public bool canExit = false;
    public bool inVehicle = false;
    public Collider vehicleCol;
    public PlayerMovementSM playsm;
    public Animator carDoorAnim;
    public RaycastMaster rMaster;

    private void Start()
    {
        vehicle.GetComponent<CarController>().enabled = false;
        vehicleCam.SetActive(false);
    }

    private void Update()
    {
        EnterVehicle();
        ExitVehicle();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canEnter = true;
        }
        if (other.CompareTag("Player") && inVehicle)
        {
            canExit = true;
        }
        else
        {
            canEnter = false;
            canExit = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canEnter = false;
    }

    public void EnterVehicle()
    {
        if (canEnter == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(EnteringVehicle());
            }
        }
    }

    IEnumerator EnteringVehicle()
    {
        rMaster.interactKey.SetActive(false);
        vehicleCol.GetComponent<Collider>().enabled = false;
        player.GetComponent<PlayerMovementSM>().enabled = false;
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<ThrowGrenade>().enabled = false;
        vehicleCam.SetActive(true);
        playerCam.SetActive(false);
        TPCam.SetActive(false);
        playsm.anim.SetBool("enteringCar", true);
        carDoorAnim.SetBool("doorOpen", true);
        AudioManager.manager.Play("CarDoor");
        yield return new WaitForSeconds(5);
        playsm.anim.SetBool("enteringCar", false);
        carDoorAnim.SetBool("doorOpen", false);
        AudioManager.manager.Stop("CarDoor");
        vehicle.GetComponent<CarController>().speedometer.SetActive(true);
        vehicle.GetComponent<CarController>().enabled = true;
        player.transform.parent = carSeat.transform;
        player.transform.rotation = carSeat.transform.rotation;
        player.transform.position = carSeat.transform.position;
        inVehicle = true;
        playsm.inVehicle = true;
        canEnter = false;
        canExit = true;
    }

    public void ExitVehicle()
    {
        if (canExit == true)
        {
            rMaster.interactKey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && inVehicle)
            {
                StartCoroutine(ExitingVehicle());
            }
        }
    }

    IEnumerator ExitingVehicle()
    {
        vehicleCol.GetComponent<Collider>().enabled = true;
        vehicle.GetComponent<CarController>().speedometer.SetActive(false);
        playsm.anim.SetBool("exitingCar", true);
        carDoorAnim.SetBool("doorOpen", true);
        AudioManager.manager.Play("CarDoor");
        yield return new WaitForSeconds(5);
        vehicleCam.SetActive(false);
        playerCam.SetActive(true);
        TPCam.SetActive(true);
        player.transform.parent = null;
        player.transform.position = exitPoint.transform.position;
        player.transform.rotation = exitPoint.transform.rotation;
        playsm.anim.SetBool("exitingCar", false);
        carDoorAnim.SetBool("doorOpen", false);
        player.GetComponent<PlayerMovementSM>().enabled = true;
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<ThrowGrenade>().enabled = true;
        vehicle.GetComponent<CarController>().enabled = false;
        inVehicle = false;
        playsm.inVehicle = false;
        canExit = false;
        rMaster.interactKey.SetActive(false);
    }
}

