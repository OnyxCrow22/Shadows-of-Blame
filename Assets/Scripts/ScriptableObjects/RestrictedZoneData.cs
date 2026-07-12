using UnityEngine;

[CreateAssetMenu(menuName = "Zones/Restricted Zone")]
public class RestrictedZoneData : ScriptableObject
{
    public string zoneName;
    public bool requiresAccess;
    public string warningMessage;
    public string punishmentMessage;
    public int policeEscalationLevel = 5;
}
