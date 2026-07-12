using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public LightState assignedState; // Set this in the Inspector (Red, Amber, or Green)
    public GameObject lightObject;

    public void UpdateLight(LightState state)
    {
        // Light is only active if the system's state matches this light's assignment
        if (lightObject != null)
            lightObject.SetActive(state == assignedState);
    }
}