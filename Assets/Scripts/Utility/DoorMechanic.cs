using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanic : MonoBehaviour
{
    public Animator doorAnim;
    public bool isOpen;
    private DoorSight interact;

    private void Start()
    {
        isOpen = false;
        interact = GetComponent<DoorSight>();
    }

    public IEnumerator OpeningDoor()
    {
        doorAnim.SetBool("openDoor", true);
        Debug.Log("DOOR OPENING");
        isOpen = true;
        yield return new WaitForSeconds(2);
        interact.interactKey.SetActive(false);
    }

    public IEnumerator ClosingDoor()
    {
        doorAnim.SetBool("closeDoor", true);
        Debug.Log("DOOR NOW CLOSING");
        isOpen = false;
        yield return new WaitForSeconds(2);
        interact.interactKey.SetActive(false);
    }
}
