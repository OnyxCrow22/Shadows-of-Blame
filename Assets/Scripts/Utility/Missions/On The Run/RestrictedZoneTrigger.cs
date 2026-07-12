using UnityEngine;
using System;

public class RestrictedZoneTrigger : MonoBehaviour
{
    [SerializeField] private RestrictedZoneData zoneData;

    public static event Action<RestrictedZoneData> OnRestrictedZoneEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Vehicle"))
            return;

        OnRestrictedZoneEntered?.Invoke(zoneData);
    }
}
