using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusSpawn : MonoBehaviour
{
    public SpawnNPC spawn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FemaleNPC") || (other.CompareTag("MaleNPC")))
        {
            StartCoroutine("Spawner");
        }
    }

    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(1);

        spawn.Spawn();
    }
}
