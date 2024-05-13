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

    private void Start()
    {
        atBottom = true;
        atTop = false;
    }

    public void Update()
    {
        if (atBottom && rMaster.buttonPressed)
        {
            OperateLift();
            player.transform.position = lift.transform.position;
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
            lift.transform.Translate(0, 0, 24);
            atBottom = false;
            atTop = true;
            rMaster.buttonPressed = false;
            player.transform.parent = null;
        }
    }

    public void GoingDown()
    {
        if (atTop && !atBottom)
        {
            atBottom = true;
            atTop = false;
        }
    }
}
