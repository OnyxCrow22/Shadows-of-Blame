using System;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [Header("Identify")]
    [SerializeField] private string zoneID; // What zone is the player currently in?
    [SerializeField] private string targetTag = "Player"; // The player is the target tag

    public static Action<string, bool> OnZoneStateChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) ||  other.CompareTag("Vehicle"))
        {
            OnZoneStateChanged?.Invoke(zoneID, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag) || other.CompareTag("Vehicle"))
        {
            OnZoneStateChanged?.Invoke(zoneID, false);
        }
    }
}