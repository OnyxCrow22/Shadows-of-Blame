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
    public Transform carSeat;
    public bool canEnter = false;
    public bool canExit = false;
    public bool inVehicle = false;
    public Collider vehicleColEnter, ExitCol;
    public PlayerMovementSM playsm;
    public Animator carDoorAnim;

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
        vehicleColEnter.GetComponent<Collider>().enabled = false;
        vehicleCam.SetActive(true);
        playerCam.SetActive(false);
        TPCam.SetActive(false);
        playsm.anim.SetBool("enteringCar", true);
        carDoorAnim.Play("CarDoor");
        yield return new WaitForSeconds(5);
        player.transform.parent = carSeat.gameObject.transform;
        player.transform.parent = carSeat;
        player.transform.rotation = carSeat.rotation;
        playsm.anim.SetBool("enteringCar", false);
        player.GetComponent<PlayerMovementSM>().enabled = false;
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        vehicle.GetComponent<CarController>().enabled = true;
        inVehicle = true;
        canEnter = false;
        canExit = true;
        ExitCol.GetComponent<Collider>().enabled = true;
    }

    public void ExitVehicle()
    {
        if (canExit == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && inVehicle)
            {
                StartCoroutine(ExitingVehicle());
            }
        }
    }

    IEnumerator ExitingVehicle()
    {
        ExitCol.GetComponent<Collider>().enabled = false;
        vehicleCam.SetActive(false);
        playerCam.SetActive(true);
        TPCam.SetActive(true);
        player.transform.parent = null;
        playsm.anim.SetBool("exitingCar", true);
        carDoorAnim.Play("CarDoor");
        yield return new WaitForSeconds(5);
        playsm.anim.SetBool("exitingCar", false);
        player.GetComponent<PlayerMovementSM>().enabled = true;
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        vehicle.GetComponent<CarController>().enabled = false;
        inVehicle = false;
        ExitCol.gameObject.SetActive(false);
        canEnter = true;
        canExit = false;
        vehicleColEnter.GetComponent<Collider>().enabled = true;
    }
}

