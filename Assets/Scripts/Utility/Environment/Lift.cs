using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class Lift : MonoBehaviour, IInteractable
{
    [Header("Lift Settings")]
    public GameObject lift;
    public GameObject player;
    public RaycastMaster rMaster;
    public Animator[] liftDoors;

    [Header("Lift Booleans")]
    public bool atTop = false;
    public bool atBottom = false;
    public float liftSpeed = 3f;
    public float travelDistance;

    [Header("Lift Vectors")]
    Vector3 topPos;
    Vector3 bottomPos;

    [Header("Floor points")]
    public Transform[] floors;
    private int currentFloor = 0;
    private bool isMoving = false;

    private void Start()
    {
        bottomPos = lift.transform.position;
        topPos = bottomPos + new Vector3(0, travelDistance, 0);
    }

    public IEnumerator OperateLift(int targetFloor)
    {
        // The lift is moving!
        isMoving = true;

        // Close all doors
        CloseDoors(currentFloor);
        yield return new WaitForSeconds(2);

        Vector3 targetPos = floors[targetFloor].position;

        while (Vector3.Distance(lift.transform.position, targetPos) > 0.01f)
        {
            lift.transform.position = Vector3.MoveTowards(lift.transform.position, targetPos, 3f * Time.deltaTime);
            yield return null;
        }

        // Lift has arrived at its destination.
        currentFloor = targetFloor;

        OpenDoors(currentFloor);

        isMoving = false;
    }

    public void OpenDoors(int floorIndex)
    {
        liftDoors[0].SetBool("openingDoors", true);
        liftDoors[floorIndex + 1].SetBool("openingDoors", true);
    }

    public void CloseDoors(int floorIndex)
    {
        liftDoors[0].SetBool("closingDoors", true);
        liftDoors[floorIndex + 1].SetBool("closingDoors", true);
    }

    public void GoToFloor(int floorIndex)
    {
        if (isMoving || floorIndex == currentFloor) return;
        StartCoroutine(OperateLift(floorIndex));
    }

    public void OnInteract() { }

    public void OnLookAt()
    {
        Debug.Log("Looking at the lift button");
    }

    public void Toggle()
    {
        if (isMoving) return;

        int nextFloor = (currentFloor + 1) % floors.Length;

        rMaster.buttonPressed = true;
        rMaster.inLift = true;

        GoToFloor(nextFloor);
    }

    public void OnLookAway() { }
}
