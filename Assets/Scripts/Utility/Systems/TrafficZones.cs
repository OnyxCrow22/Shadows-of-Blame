using UnityEngine;

public class TrafficZones : MonoBehaviour
{
    // Assign all traffic systems in this area in the Inspector
    public TrafficSystem[] intersections;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var system in intersections)
            {
                system.enabled = true; // Enable the logic for all systems in this zone
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var system in intersections)
            {
                system.enabled = false; // Disable to save performance
            }
        }
    }
}