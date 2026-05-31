using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    public Transform rotationTarget;
    // Update is called once per frame
    void Update()
    {
        RotateCamera();   
    }

    void RotateCamera()
    {
        transform.eulerAngles = new Vector3(90, rotationTarget.eulerAngles.y, 0);
    }    
}
