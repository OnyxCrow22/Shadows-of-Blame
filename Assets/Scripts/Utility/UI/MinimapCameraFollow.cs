using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        Vector3 rot = transform.eulerAngles;
        rot.y = target.eulerAngles.y;
        transform.eulerAngles = rot;
    }
}
