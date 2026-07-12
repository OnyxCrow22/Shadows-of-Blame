using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class RespawnManager : MonoBehaviour
{
    public Transform[] hospitalSpawnPoints;
    public PlayerState playerState;
    public HealthSystem playerHealth;
    public PlayerMovementSM movement;

    public float respawnDelay = 3f;

    public void HandlePlayerDeath()
    {
        StartCoroutine(RespawnSequence());
    }

    private IEnumerator RespawnSequence()
    {
        movement.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        playerHealth.ResetHealth();

        Transform spawnPoint = ChooseHospitalSpawn();
        movement.player.transform.position = spawnPoint.position;
        Physics.SyncTransforms();

        movement.enabled = true;

        MissionEvents.RaisePlayerRespawned();
    }

    private Transform ChooseHospitalSpawn()
    {
        if (playerState.HasWesteriaAccess || playerState.IsInWestInsbury)
            return hospitalSpawnPoints[Random.Range(0, hospitalSpawnPoints.Length)];

        return hospitalSpawnPoints[0]; // Saint Mary's
    }
}
