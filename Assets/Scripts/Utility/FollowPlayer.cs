using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float offsetX, offsetZ;
    public float lerpSpeed;

    private void LateUpdate()
    {
        MinimapFollow();
    }

    void MinimapFollow()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offsetX, transform.position.y, target.position.z + offsetZ), lerpSpeed);
    }
}
