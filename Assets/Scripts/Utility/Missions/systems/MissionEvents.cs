public static class MissionEvents
{
    public static System.Action OnGangLeaderKilled;
    public static System.Action OnGangMemberKilled;
    public static System.Action<string> OnEvidenceCollected;
    public static System.Action<string> OnPlayerEnteredZone;
    public static System.Action<string> OnPlayerExitedZone;
    public static System.Action<bool> OnPoliceTriggered;
    public static System.Action<bool> OnObjectiveComplete;
    public static System.Action RaisePoliceEvaded;
    public static System.Action RaisePlayerRespawned;
}