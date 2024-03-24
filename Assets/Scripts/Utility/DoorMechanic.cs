using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanic : MonoBehaviour
{
    public GameObject door;
    public GameObject fpsCamera;
    public GameObject interactKey;
    public Animator doorAnim;
    bool isOpen;

    private void Start()
    {
        interactKey.SetActive(false);
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoor();
        CloseDoor();
    }

    void OpenDoor()
    {
        RaycastHit doorHit;
        if (Physics.Raycast(fpsCamera.transform.position, door.transform.position, out doorHit))
        {
            StartCoroutine(OpeningDoor());
        }
    }

    void CloseDoor()
    {
        RaycastHit doorHit;
        if(Physics.Raycast(fpsCamera.transform.position, door.transform.position, out doorHit))
        {
            StartCoroutine(ClosingDoor());
        }
    }

    IEnumerator OpeningDoor()
    {
        interactKey.SetActive(true);
        doorAnim.SetBool("openDoor", true);
        Debug.Log("DOOR OPENING");
        isOpen = true;
        yield return new WaitForSeconds(2);
        interactKey.SetActive(false);
    }

    IEnumerator ClosingDoor()
    {
        interactKey.SetActive(true);
        doorAnim.SetBool("closeDoor", true);
        Debug.Log("DOOR NOW CLOSING");
        isOpen = false;
        yield return new WaitForSeconds(2);
        interactKey.SetActive(false);
    }
}
