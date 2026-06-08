using UnityEngine;

[CreateAssetMenu(fileName = "BiomeDatabase", menuName = "World/BiomeDatabase")]
public class BiomeDatabase : ScriptableObject
{
    public BiomeRules[] biomes;

    public BiomeRules GetBiomeByColour(Color c)
    {
            BiomeRules closest = null;
        float closestDist = float.MaxValue;

        foreach (var biome in biomes)
        {
            float dist = Vector3.Distance(
                new Vector3(c.r, c.g, c.b),
                new Vector3(biome.color.r, biome.color.g, biome.color.b)
        );

        if (dist < closestDist)
        {
            closestDist = dist;
            closest = biome;
        }
    }

    return closest;
    }
}
