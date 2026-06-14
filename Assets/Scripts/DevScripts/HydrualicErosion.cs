using System.Collections;
using UnityEngine;

public static class HydrualicErosion
{
    public static void Erode(WorldData world)
    {
        int size = world.Resolution;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0;  y < size; y++)
            {
                float flow = world.flow[x, y];
                float slope = world.slope[x, y];

                float flowStrength = Mathf.Log(flow + 1f);

                float erosion = flowStrength * slope * 0.0005f;

                world.height[x, y] -= erosion;

                world.height[x, y] = Mathf.Clamp01(world.height[x, y]);
            }
        }
    }
}