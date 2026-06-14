using System.Collections;
using UnityEngine;

public static class FlowMapGenerator
{
    static readonly Vector2Int[] dirs =
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 1),
        new Vector2Int(-1, -1),
        new Vector2Int(1, -1),
        new Vector2Int (-1, 1),
    };

    public static void Generate(WorldData world)
    {
        int size = world.Resolution;

        for (int x = 1;  x < size - 1; x++)
        {
            for (int y = 1; y < size - 1; y++)
            {
                float currentHeight = world.height[x, y];

                float lowestHeight = currentHeight;
                Vector2Int bestDir = Vector2Int.zero;

                foreach (var  dir in dirs)
                {
                    int nx = x + dir.x;
                    int ny = y + dir.y;

                    if (world.height[nx, ny] < lowestHeight)
                    {
                        lowestHeight = world.height[nx, ny];
                        bestDir = dir;
                    }
                }

                world.flowDir[x, y] = bestDir;
            }
        }

        AccumlateFlow(world);
    }

    static void AccumlateFlow(WorldData world)
    {
        int size = world.Resolution;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                world.flow[x, y] = 0;
            }
        }

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                float water = 1f;

                int cx = x;
                int cy = y;

                for (int i = 0; i < size; i++)
                {
                    world.flow[cx, cy] += water;

                    water *= 0.95f;

                    if (water < 0.01f)
                        break;

                    Vector2Int dir = world.flowDir[cx, cy];

                    if (dir == Vector2Int.zero)
                        break;

                    cx += dir.x;
                    cy += dir.y;

                    if (cx < 0 || cy < 0 || cx >= size || cy >= size)
                        break;
                }
            }
        }
    }
}