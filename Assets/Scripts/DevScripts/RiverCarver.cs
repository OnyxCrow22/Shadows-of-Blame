using System.Collections;
using UnityEngine;

public static class RiverCarver
{
    public static void Carve(WorldData world)
    {
        int size = world.Resolution;

        for (int x = 1; x < size - 1; x++)
        {
            for (int y = 1;  y < size - 1; y++)
            {
                if (!world.rivers[x, y])
                    continue;

                float strength = world.riverStrength[x, y];

                float depth = Mathf.Lerp(0.001f, 0.008f, strength);
                float width = Mathf.Lerp(1, 3, Mathf.Sqrt(world.flow[x, y]));

                world.height[x, y] -= depth * 0.7f;

                world.height[x, y] = Mathf.Clamp01(world.height[x, y]);

                int radius = Mathf.CeilToInt(width);

                for (int dx = -radius; dx <= radius; dx++)
                {
                    for (int dy = -radius; dy <= radius; dy++)
                    {
                        int nx = x + dx;
                        int ny = y + dy;

                        if (nx < 0 || ny < 0 || nx >= size || ny >= size)
                            continue;

                        float distance = Mathf.Abs(dx) + Mathf.Abs(dy);
                        float fallOff = Mathf.Clamp01(1f - (distance / width));

                        world.height[nx, ny] -= depth * 0.3f * fallOff;
                        world.height[nx, ny] = Mathf.Clamp01(world.height[nx, ny]);
                    }
                }
            }
        }
    }
}