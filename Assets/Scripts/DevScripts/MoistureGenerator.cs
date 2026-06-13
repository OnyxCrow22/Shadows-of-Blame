using System.Collections;
using UnityEngine;

public static class MoistureGenerator
{
    public static void Generate(WorldData world)
    {
        float scale = 0.01f;

        for (int x = 0; x < world.Resolution; x++)
        {
            for (int y = 0; y < world.Resolution; y++)
            {
                float height = world.height[x, y];

                float noise = Mathf.PerlinNoise(x *  scale, y * scale);

                float heightFactor = 1f - height;

                float oceanFactor = 1f - height;

                float moisture =
                    (noise * 0.4f) +
                    (heightFactor * 0.3f) +
                    (oceanFactor * 0.3f);

                world.moisture[x, y] = Mathf.Clamp01(moisture);
            }
        }
    }

    public static float OceanProximity(WorldData world, int x, int y)
    {
        int size = world.Resolution;

        float minDist = float.MaxValue;

        for (int i = 0; i < size; i++)
        {
            Check(x, y, i, 0, ref minDist, world);
            Check(x, y, i, size - 1, ref minDist, world);
            Check(x, y, 0, i, ref minDist, world);
            Check(x, y, size - 1, i, ref minDist, world);
        }

        float maxDist = size * 0.5f;
        return 1f - Mathf.Clamp01(minDist / maxDist);
    }

    private static void Check(int x, int y, int ox, int oy, ref float minDist, WorldData world)
    {
        if (world.height[ox, oy] < 0.3f)
        {
            float dx = x - ox;
            float dy = y - oy;
            float dist = dx * dx + dy * dy;

            if (dist < minDist)
                minDist = dist;
        }
    }
}
