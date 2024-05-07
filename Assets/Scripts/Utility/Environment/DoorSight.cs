using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public Animator doorAnim;
    public RaycastMaster rMaster;
    public AudioSource doorSound;

    // Use this only for buttons, otherwise leave blank.
    public GameObject doorReference;

    // Use the same sound twice in the array if a garage door.
    public AudioClip[] doorClips;

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
