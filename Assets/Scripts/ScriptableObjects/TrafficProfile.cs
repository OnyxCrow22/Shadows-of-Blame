using UnityEngine;

[CreateAssetMenu(fileName = "NewTrafficProfile", menuName = "ShadowsOfBlame/Traffic Profile")]
public class TrafficProfile : ScriptableObject
{
    public float redDuration;
    public float amberDuration;
    public float greenDuration;
}