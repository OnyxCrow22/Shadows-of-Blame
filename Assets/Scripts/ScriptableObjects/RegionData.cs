using UnityEngine;

[CreateAssetMenu(menuName = "ShadowsOfBlame/City Data")]
public class RegionData : ScriptableObject
{
    public string cityName;
    public string[] districtNames;
    public string[] roadNames;
}
