using System.Collections;
using UnityEngine;

public class Lift : MonoBehaviour, IInteractable
{
    public bool isTriggered { get; set; }
    public RaycastMaster rMaster;
    public Animator[] liftDoors;
    public Transform[] floors;
    public float liftSpeed = 3f;

    private int currentFloor = 0;
    private bool isMoving = false;

    public void OnInteract(GameObject user)
    {
        Toggle();
    }

    public void Toggle()
    {
        int nextFloor = (currentFloor + 1) % floors.Length;
        MoveToFloor(nextFloor);
    }

    public void MoveToFloor(int floorIndex)
    {
        if (isMoving || floorIndex == currentFloor) return;

        // Centralized state handling
        if (rMaster != null)
        {
            rMaster.buttonPressed = true;
            rMaster.inLift = true;
        }

        StartCoroutine(OperateLift(floorIndex));
    }

    private IEnumerator OperateLift(int targetFloor)
    {
        isMoving = true;
        SetDoorState(currentFloor, false); // Close
        yield return new WaitForSeconds(2);

        while (Vector3.Distance(transform.position, floors[targetFloor].position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, floors[targetFloor].position, liftSpeed * Time.deltaTime);
            yield return null;
        }

        currentFloor = targetFloor;
        SetDoorState(currentFloor, true); // Open
        isMoving = false;
    }

    private void SetDoorState(int floorIndex, bool open)
    {
        string state = open ? "openingDoors" : "closingDoors";
        liftDoors[0].SetBool(state, open);
        if (floorIndex + 1 < liftDoors.Length)
            liftDoors[floorIndex + 1].SetBool(state, open);
    }

    public void OnLookAt() { }
    public void OnLookAway() { }
}