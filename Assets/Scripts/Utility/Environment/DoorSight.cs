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
        CheckDoor();
    }

    void CheckDoor()
    {
        Ray doorRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        RaycastHit doorHit;
        float RayLength = 4;
        if (Physics.Raycast(doorRay, out doorHit, RayLength))
        {
            if (doorHit.collider.gameObject.tag == "Door")
            {
                GameObject door = doorHit.collider.transform.root.gameObject;
                doorAnim = door.GetComponent<Animator>();
                interactKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && isOpen)
                {
                    StartCoroutine(ClosingDoor());
                    StopCoroutine(OpeningDoor());
                }
                else if (Input.GetKeyDown(KeyCode.E) && !isOpen)
                {
                    StartCoroutine(OpeningDoor());
                    StopCoroutine(ClosingDoor());
                }

            }
            else if (doorHit.collider.gameObject.tag != "Door")
            {
                interactKey.SetActive(false);
            }
        }
    }

    public IEnumerator OpeningDoor()
    {
        doorAnim.SetBool("openDoor", true);
        doorAnim.SetBool("closeDoor", false);
        Debug.Log("DOOR OPENING");
        isOpen = true;
        yield return new WaitForSeconds(2);
        interactKey.SetActive(false);
        StopCoroutine(OpeningDoor());
    }

    public IEnumerator ClosingDoor()
    {
        doorAnim.SetBool("closeDoor", true);
        doorAnim.SetBool("openDoor", false);
        Debug.Log("DOOR NOW CLOSING");
        isOpen = false;
        yield return new WaitForSeconds(2);
        interactKey.SetActive(false);
        StopCoroutine(ClosingDoor());
    }
}
