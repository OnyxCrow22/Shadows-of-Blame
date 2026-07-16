using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ShadowsOfBlame/City Data")]
public class RegionData : ScriptableObject
{
    public string cityName;
    public string[] districtNames;
    public List<string> streetNames;
}
