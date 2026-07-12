using UnityEngine;

public class GangMemberLogic : MonoBehaviour
{
    public EnemyMovementSM esm;
    public Collider capsule;
    public bool isDead = false;

    public void OnDeath()
    {
        if (isDead) return;

        if (esm != null && esm.eHealth.health <= 0)
        {
            isDead = true;
            if (capsule != null) capsule.enabled = false;

            // Broadcast: "A member has died!"
            // The central MissionManager handles the count and UI
            MissionEvents.OnGangMemberKilled?.Invoke();
        }
    }
}