using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "WorldGen/Biome Rule")]
public class BiomeRules : ScriptableObject
{
    public BiomeType biome;

    [Header("Height")]
    public float minHeight;
    public float maxHeight;

    [Header("Slope")]
    public float minSlope;
    public float maxSlope;

    [Header("Moisture")]
    public float minMoisture;
    public float maxMoisture;

    [Header("Priority")]
    public int priority;
}