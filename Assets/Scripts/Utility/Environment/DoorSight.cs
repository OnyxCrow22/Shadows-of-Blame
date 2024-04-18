using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public Animator doorAnim;

    public IEnumerator OpeningDoor()
    {
        doorAnim.SetBool("openDoor", true);
        doorAnim.SetBool("closeDoor", false);
        Debug.Log("DOOR OPENING");
        isOpen = true;
        yield return new WaitForSeconds(2);
        StopCoroutine(OpeningDoor());
    }

    public IEnumerator ClosingDoor()
    {
        doorAnim.SetBool("closeDoor", true);
        doorAnim.SetBool("openDoor", false);
        Debug.Log("DOOR NOW CLOSING");
        isOpen = false;
        yield return new WaitForSeconds(2);
        StopCoroutine(ClosingDoor());
    }
}
