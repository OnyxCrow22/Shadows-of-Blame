using System.Collections;
using UnityEngine;

public static class HeightGenerator
{
    public static void Generate(WorldData world)
    {
        float scale = 0.005f;

        for (int x = 0; x < world.Resolution; x++)
        {
            for (int y = 0; y < world.Resolution; y++)
            {
                float nx = x * scale;
                float ny = y * scale;

                float height = Mathf.PerlinNoise(nx, ny);

                float centreX = world.Resolution / 2;
                float centreY = world.Resolution / 2;

                float dx = (x - centreX) / centreX;
                float dy = (y - centreY) / centreY;

                float distance = Mathf.Sqrt(dx * dx + dy * dy);

                height -= distance;
                height = Mathf.Clamp01(height);

                world.height[x, y] = height;
            }
        }
    }
}