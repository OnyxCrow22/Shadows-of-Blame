using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public Animator doorAnim;
    public AudioSource doorSound;
    public AudioClip[] doorClips;

    public IEnumerator OpeningDoor()
    {
        doorAnim.SetBool("openDoor", true);
        doorSound.PlayOneShot(doorClips[0]);
        doorAnim.SetBool("closeDoor", false);
        Debug.Log("DOOR OPENING");
        isOpen = true;
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
        yield return new WaitForSeconds(2);
        StopCoroutine(ClosingDoor());
    }
}
