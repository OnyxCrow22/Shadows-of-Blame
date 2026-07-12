using UnityEngine;

public class VehicleSensor : MonoBehaviour
{
    public bool PlayerAhead { get; private set; }
    public bool NPCAhead { get; private set; }
    public bool RedLightAhead { get; private set; }

    public Transform sensorOrigin;

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(sensorOrigin.position, sensorOrigin.forward, out hit, 20f))
        {
            PlayerAhead = hit.collider.CompareTag("Player");
            NPCAhead = hit.collider.CompareTag("NPC");
            RedLightAhead = hit.collider.CompareTag("TrafficLightChecker");
        }
        else
        {
            PlayerAhead = NPCAhead = RedLightAhead = false;
        }
    }
}
