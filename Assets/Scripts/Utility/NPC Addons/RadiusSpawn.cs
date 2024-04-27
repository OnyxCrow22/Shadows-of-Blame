using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusSpawn : MonoBehaviour
{
    public SpawnNPC spawner;
    public RemoveNPC remover;

    private void OnTriggerStay(Collider other)
    {
        spawner.StartCoroutine(spawner.Spawn());

        if (spawner.AICount > 75)
        {
            spawner.StopCoroutine(spawner.Spawn());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        remover.OnBecameInvisible();
    }
}
