using System.Collections;
using UnityEngine;

public static class TerrainAnalysis
{
    public static void CalculateSlope(WorldData world)
    {
        for (int x = 1; x < world.Resolution - 1; x++)
        {
            for (int y = 1;  y < world.Resolution - 1; y++)
            {
                float dx =
                    world.height[x + 1, y] -
                    world.height[x - 1, y];

                float dy =
                    world.height[x, y + 1] -
                    world.height[x, y - 1];

                world.slope[x, y] =
                    Mathf.Sqrt(dx * dx + dy * dy);
            }
        }
    }
}