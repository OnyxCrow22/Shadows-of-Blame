using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    public bool isAnimating = false;
    public AudioClip[] doorClips;
    public AudioSource doorSound;
    public Animator doorAnim;
    public RaycastMaster rMaster;

    public void OnInteract() {  }

    public void OnLookAt() { }

    public void OnLookAway() { }

    public void Toggle()
    {
        if (isAnimating) return;

        if (isOpen)
        {
            StartCoroutine(DoorRoutine("closeDoor", doorClips[1], false));
        }
        else
        {
            StartCoroutine(DoorRoutine("openDoor", doorClips[0], true));
        }
    }

    public IEnumerator DoorRoutine(string animParam, AudioClip clip, bool openState)
    {
        isAnimating = true;
        isOpen = openState;

        doorAnim.SetBool("closeDoor", false);
        doorAnim.SetBool("openDoor", false);

        doorAnim.SetBool(animParam, true);

        if (clip != null) doorSound.PlayOneShot(clip);

        rMaster.interactKey.SetActive(false);

        yield return new WaitForSeconds(2f);

        isAnimating = false;
    }
}
