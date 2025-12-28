using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    public Animator doorAnim;
    public RaycastMaster rMaster;
    public AudioSource doorSound;

    public GameObject interactKey;

    // Use this only for buttons, otherwise leave blank.
    public GameObject doorReference;

    // Use the same sound twice in the array if a garage door.
    public AudioClip[] doorClips;

    public void OnInteract()
    {
        Toggle();
        if (interactKey != null)
        {
            interactKey.SetActive(false);
        }
    }

    public void OnLookAt()
    {
        if (interactKey != null)
        {
            interactKey.SetActive(true);
        }
    }

    public void OnLookAway()
    {
        if (interactKey != null)
        {
            interactKey.SetActive(false);
        }
    }

    public void Toggle()
    {
        if (isOpen)
        {
            StartCoroutine(ClosingDoor());
            StopCoroutine(OpeningDoor());

        }
        else
        {
            StartCoroutine(OpeningDoor());
            StopCoroutine(ClosingDoor());
        }
    }

    public IEnumerator OpeningDoor()
    {
        doorAnim.SetBool("openDoor", true);
        doorSound.PlayOneShot(doorClips[0]);
        doorAnim.SetBool("closeDoor", false);
        Debug.Log("DOOR OPENING");
        isOpen = true;
        rMaster.interactKey.SetActive(false);
        yield return new WaitForSeconds(2);
        StopCoroutine(OpeningDoor());
    }

    public IEnumerator ClosingDoor()
    {
        doorAnim.SetBool("closeDoor", true);
        doorSound.PlayOneShot(doorClips[1]);
        doorAnim.SetBool("openDoor", false);
        Debug.Log("DOOR NOW CLOSING");
        isOpen = false;
        rMaster.interactKey.SetActive(false);
        yield return new WaitForSeconds(2);
        StopCoroutine(ClosingDoor());
    }
}
