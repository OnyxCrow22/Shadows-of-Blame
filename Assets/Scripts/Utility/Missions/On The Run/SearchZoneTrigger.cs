using UnityEngine;
using System;

public class SearchZoneTrigger : MonoBehaviour
{
    [SerializeField] private string zoneID;

    public static event Action<string> OnSearchZoneEntered;
    public static event Action<string> OnSearchZoneExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnSearchZoneEntered?.Invoke(zoneID);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            OnSearchZoneExited?.Invoke(zoneID);
    }
}
