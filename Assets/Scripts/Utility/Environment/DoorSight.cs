using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    // Required by interface
    public bool isTriggered { get; set; }

    public bool isOpen = false;
    public bool isAnimating = false;
    public AudioClip[] doorClips;
    public AudioSource doorSound;
    public Animator doorAnim;
    public RaycastMaster rMaster;

    // Interface method with required parameter
    public void OnInteract(GameObject interactedObj)
    {
        Toggle();
    }

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

        if (rMaster != null) rMaster.interactKey.SetActive(false);

        yield return new WaitForSeconds(2f);

        isAnimating = false;
    }
}