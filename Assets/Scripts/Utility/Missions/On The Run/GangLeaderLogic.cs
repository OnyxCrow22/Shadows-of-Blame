using UnityEngine;

public class GangLeaderLogic : MonoBehaviour
{
    public EnemyMovementSM esm;
    public bool isDead = false;

    // Call this from health system or state machine when taking damage/dying
    public void Check()
    {
        if (isDead) return;

        if (esm != null && esm.eHealth != null && esm.eHealth.health <= 0)
        {
            isDead = true;

            // Simply broadcast to the entire game: "The leader is dead!"
            MissionEvents.OnGangLeaderKilled?.Invoke();
        }
    }
}