using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSight : MonoBehaviour
{
    private GameObject door;
    public GameObject interactKey;
    public PlayerMovementSM playsm;
    private DoorMechanic doorHandle;

    public void OpenDoor()
    {
        RaycastHit doorHit;
        Ray doorRay = new Ray(playsm.FOV.transform.position, Vector3.forward);
        Debug.DrawRay(playsm.FOV.transform.position, Vector3.forward);
        float RayLength = 5;
        if (Physics.Raycast(doorRay, out doorHit, RayLength))
        {
            if (doorHit.collider.CompareTag("Door"))
            {
                interactKey.SetActive(true);
                door = doorHit.collider.gameObject;
                if (Input.GetKeyDown(KeyCode.E) && !doorHandle.isOpen)
                {
                    StartCoroutine(doorHandle.OpeningDoor());
                }

            }
        }
    }

   public void CloseDoor()
    {
        RaycastHit doorHit;
        Ray doorRay = new Ray(playsm.FOV.transform.position, Vector3.forward);
        float RayLength = 5;
        if (Physics.Raycast(doorRay, out doorHit, RayLength))
        {
            if (doorHit.collider.CompareTag("Door"))
            {
                interactKey.SetActive(true);
                door = doorHit.collider.gameObject;
                if (Input.GetKeyDown(KeyCode.E) && doorHandle.isOpen)
                {
                    StartCoroutine(doorHandle.ClosingDoor());
                }
            }
        }
    }
}
