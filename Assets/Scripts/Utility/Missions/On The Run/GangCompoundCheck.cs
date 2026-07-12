using UnityEngine;

public class GangCompoundCheck : MonoBehaviour
{
    // No more reference to OnTheRun!
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Just broadcast: "Player arrived at compound"
            MissionEvents.OnPlayerEnteredZone?.Invoke("GangCompound");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Just broadcast: "Player left compound"
            MissionEvents.OnPlayerExitedZone?.Invoke("GangCompound");
        }
    }
}