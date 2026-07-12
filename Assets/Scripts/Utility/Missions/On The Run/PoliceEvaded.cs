using UnityEngine;

public class PoliceEvaded : MonoBehaviour
{
    [SerializeField] private PoliceLevel police;

    public bool HasLostPolice { get; private set; }

    /// <summary>
    /// Call this when the player has successfully evaded the police.
    /// </summary>
    public void OnPoliceEvaded()
    {
        if (PoliceLevel.policeLevels >= 1)
        {
            HasLostPolice = true;

            // Stop activating higher levels once evaded
            PoliceLevel.activateLevel = false;

            // Sync any visual / HUD representation of police level
            police.UpdateLevel();

            // Notify mission flow
            MissionEvents.RaisePoliceEvaded();
        }
    }
}
