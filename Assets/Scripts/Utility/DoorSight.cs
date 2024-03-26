using System.Collections;
using UnityEngine;

public class DoorSight : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject interactKey;
    public PlayerMovementSM playsm;
    Animator doorAnim;

    private void Update()
    {
        OpenDoor();
        CloseDoor();
    }

    void OpenDoor()
    {
        Ray doorRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        RaycastHit doorHit;
        float RayLength = 2;
        if (Physics.Raycast(doorRay, out doorHit, RayLength))
        {
            if (doorHit.collider.gameObject.tag == "Door")
            {
                GameObject door = doorHit.collider.transform.root.gameObject;
                doorAnim = door.GetComponent<Animator>();
                interactKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(OpeningDoor());
                }

            }
        }
        /*
        if (doorHit.collider.gameObject.tag != "Door")
        {
            interactKey.SetActive(false);
        }
        */
    }

    void CloseDoor()
    {
        Ray doorRay = new Ray(transform.position, transform.forward);
        RaycastHit doorHit;
        float RayLength = 2;
        if (Physics.Raycast(doorRay, out doorHit, RayLength))
        {
            if (doorHit.collider.gameObject.tag == "Door")
            {
                GameObject door = doorHit.collider.transform.root.gameObject;
                doorAnim = door.GetComponent<Animator>();
                interactKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(ClosingDoor());
                }
            }
        }
        /*
        if (doorHit.collider.gameObject.tag != "Door")
        {
            interactKey.SetActive(false);
        }
        */
    }

    public IEnumerator OpeningDoor()
    {
        doorAnim.SetBool("openDoor", true);
        Debug.Log("DOOR OPENING");
        isOpen = true;
        yield return new WaitForSeconds(2);
        interactKey.SetActive(false);
    }

    public IEnumerator ClosingDoor()
    {
        doorAnim.SetBool("closeDoor", true);
        Debug.Log("DOOR NOW CLOSING");
        isOpen = false;
        yield return new WaitForSeconds(2);
        interactKey.SetActive(false);
    }
}
