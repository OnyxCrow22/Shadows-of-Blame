using System.Collections;
using UnityEngine;

public static class BiomeGenerator
{
    public static BiomeRules[] rules;

    public static void Generate(WorldData world)
    {
        for (int x = 0; x < world.Resolution; x++)
        {
            for (int y = 0;  y < world.Resolution; y++)
            {
                float h = world.height[x, y];
                float s = world.slope[x, y];
                float m = world.moisture[x, y];

                BiomeType biome = EvaluateRule(h, s, m);

                world.biomes[x, y] = biome;
            }
        }
    }

    private static BiomeType EvaluateRule(float height, float slope, float moisture)
    {
        BiomeRules bestRule = null;
        int bestPriority = int.MinValue;

        foreach (var rule in rules)
        {
            if (!Matches(rule, height, slope, moisture))
                continue;

            if (rule.priority > bestPriority)
            {
                bestPriority = rule.priority;
                bestRule = rule;
            }
        }
        return bestRule != null ? bestRule.biome : BiomeType.Grasslands;
    }

    private static bool Matches(BiomeRules rule, float height, float slope, float moisture)
    {
        return
            height >= rule.minHeight && height <= rule.maxHeight &&
            slope >= rule.minSlope && slope <= rule.maxSlope &&
            moisture >= rule.minMoisture && moisture <= rule.maxMoisture;
    }
}