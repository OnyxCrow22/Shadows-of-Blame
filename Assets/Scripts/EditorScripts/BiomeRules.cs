using UnityEngine;

[CreateAssetMenu(fileName = "BiomeRules", menuName = "World/BiomeRules")]
public class BiomeRules : ScriptableObject
{
    [Header("World Control Map Colour")]
    public Color color;

    [Header("Height settings")]
    public float baseHeight = 0f;
    public float noiseFrequency = 0.01f;
    public float noiseAmptitude = 10f;

    [Header("Smoothing and Erosion")]
    public float smoothing = 1f;
    public float erosionStrength = 0;

    [Header("Height curve")]
    public AnimationCurve height = AnimationCurve.Linear(0, 0, 1, 1);
}
