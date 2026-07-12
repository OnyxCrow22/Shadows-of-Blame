using UnityEngine;
using System.Collections;

public enum LightState { Red, Amber, Green }

public class TrafficSystem : MonoBehaviour
{
    public TrafficProfile profile;
    private bool isActive = false;
    public LightState currentState = LightState.Red;
    private Coroutine trafficCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            isActive = true;
            trafficCoroutine = StartCoroutine(TrafficCycle());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            isActive = false;
            StopCoroutine(trafficCoroutine);
            SetState(LightState.Red); // Reset to default
        }
    }

    private IEnumerator TrafficCycle()
    {
        while (true)
        {
            SetState(LightState.Red);
            yield return new WaitForSeconds(profile.redDuration); // Use SO data

            SetState(LightState.Amber);
            yield return new WaitForSeconds(profile.amberDuration); // Use SO data

            SetState(LightState.Green);
            yield return new WaitForSeconds(profile.greenDuration); // Use SO data
        }
    }

    private void SetState(LightState newState)
    {
        currentState = newState;
        // Broadcast the change to all lights in this intersection
        foreach (var light in GetComponentsInChildren<TrafficLightController>())
        {
            light.UpdateLight(currentState);
        }
    }
}