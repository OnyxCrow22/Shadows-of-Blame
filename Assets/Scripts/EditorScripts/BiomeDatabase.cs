using UnityEngine;

public struct BiomeBlendInfo
{
    public BiomeRules primaryBiome;
    public BiomeRules secondaryBiome;
    public float blendFactor;
}

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

    public BiomeBlendInfo GetBiomeBlend(Color c)
    {
        BiomeBlendInfo biomeInformation = new BiomeBlendInfo();

        if (biomes.Length == 0 || biomes == null) return biomeInformation;
        if (biomes.Length == 1)
        {
            biomeInformation.primaryBiome = biomes[0];
            biomeInformation.secondaryBiome = biomes[0];
            biomeInformation.blendFactor = 0;
            return biomeInformation;
        }

        BiomeRules primary = null;
        BiomeRules secondary = null;
        float firstMinDist = float.MaxValue;
        float secondMinDist = float.MaxValue;

        foreach (var biome in biomes)
        {
            float dist = Vector3.Distance(new Vector3(c.r, c.g, c.b), new Vector3(biome.color.r, biome.color.g, biome.color.b));

            if (dist < firstMinDist)
            {
                
            }
        }
    }
}
