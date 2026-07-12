using UnityEngine;

[CreateAssetMenu(fileName = "NewTrafficProfile", menuName = "Traffic/Profile")]
public class TrafficProfile : ScriptableObject
{
    public float redDuration;
    public float amberDuration;
    public float greenDuration;
}