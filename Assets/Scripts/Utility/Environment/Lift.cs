using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Lift : MonoBehaviour
{
    public GameObject lift;
    public GameObject player;
    public RaycastMaster rMaster;
    public bool atTop = false;
    public bool atBottom = false;
    public float liftSpeed = 3f;
    Vector3 targetPos;

    private void Start()
    {
        atBottom = true;
        atTop = false;
        targetPos = lift.transform.position;
    }

    public void Update()
    {
        if (lift.transform.position != targetPos)
        {
            lift.transform.position = Vector3.MoveTowards(lift.transform.position, targetPos, liftSpeed * Time.deltaTime);
        }

        if (atBottom && rMaster.buttonPressed)
        {
            OperateLift();
            player.transform.SetParent(lift.transform);
        }
        else if (atTop && rMaster.buttonPressed)
        {
            GoingDown();
        }
    }

    public void OperateLift()
    {
        if (!atTop && atBottom)
        {
            targetPos = lift.transform.position + new Vector3(0, 48.5f, 0);
            atBottom = false;
            atTop = true;
            rMaster.buttonPressed = false;
            player.transform.SetParent(lift.transform, false);
        }
    }

    public void GoingDown()
    {
        if (atTop && !atBottom)
        {
            targetPos = lift.transform.position - new Vector3(0, 48.5f, 0);
            atBottom = true;
            atTop = false;
        }
    }
}
