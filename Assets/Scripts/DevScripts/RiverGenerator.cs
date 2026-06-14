using System.Collections;
using UnityEngine;

public static class RiverGenerator
{
    public static void Generate(WorldData world)
    {
        int size = world.Resolution;

        float maxFlow = 0;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                world.rivers[x, y] = false;

                if (world.flow[x, y] > maxFlow)
                    maxFlow = world.flow[x, y];
            }
        }

        if (maxFlow <= 0)
            return;

        float riverThreshold = 0.15f;
        float threshold = maxFlow * riverThreshold;

        for (int x = 0;x < size; x++)
        {
            for (int y = 0;y < size; y++)
            {
                if (world.rivers[x, y] =
                    world.flow[x, y] > threshold &&
                    world.height[x, y] > 0.3f)
                {
                    world.rivers[x, y] = true;

                    world.riverStrength[x, y] =
                        world.flow[x, y] / maxFlow;
                }
            }
        }
    }
}