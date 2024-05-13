using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVRotate : MonoBehaviour
{
    public GameObject PlayerFOV;
    public Camera playerCam;

    private void Update()
    {
        RotateFOV();
    }

    public void RotateFOV()
    {
        PlayerFOV.transform.rotation = playerCam.transform.rotation;
    }
}
