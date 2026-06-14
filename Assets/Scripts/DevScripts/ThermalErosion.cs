using System.Collections;
using UnityEngine;

public static class ThermalErosion
{
    public static void Erode(WorldData world)
    {
        int size = world.Resolution;

        float talusAngle = 0.02f;
        float strength = 0.5f;

        float[,] delta = new float[size, size];

        for (int x = 1; x < size; x++)
        {
            for (int y = 1; y < size; y++)
            {
                float current = world.height[x, y];

                ApplyThermal(world, delta, x, y, x + 1, y, current, talusAngle, strength);
                ApplyThermal(world, delta, x, y, x - 1, y, current, talusAngle, strength);
                ApplyThermal(world, delta, x, y, x, y + 1, current, talusAngle, strength);
                ApplyThermal(world, delta, x, y, x, y - 1, current, talusAngle, strength);
            }
        }

        for (int x = 0;  x < size; x++)
        {
            for (int y = 0;  y < size; y++)
            {
                world.height[x, y] += delta[x, y];
                world.height[x, y] = Mathf.Clamp01(world.height[x, y]);
            }
        }
    }

    public static void ApplyThermal(
        WorldData world,
        float[,] delta,
        int x,
        int y,
        int nx,
        int ny,
        float current,
        float talusAngle,
        float strength)
    {
        float neighbour = world.height[nx, ny];

        float difference = current - neighbour;

        if (difference > talusAngle)
        {
            float moved = (difference - talusAngle) * strength;

            delta[x, y] -= moved;
            delta[nx, ny] += moved;
        }
    }
}