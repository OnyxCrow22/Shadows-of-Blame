using System;
using UnityEngine;

public class SafehouseZone : MonoBehaviour
{
    public static event Action<bool> OnSafehouseStateChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        OnSafehouseStateChanged?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        OnSafehouseStateChanged?.Invoke(false);
    }
}
