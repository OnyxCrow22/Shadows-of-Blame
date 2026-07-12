using UnityEngine;
using System;

public class RemoveNPC : MonoBehaviour
{
    public static event Action<GameObject> OnNPCDespawnRequested;

    public void RequestDespawn()
    {
        OnNPCDespawnRequested?.Invoke(gameObject);
    }
}
