using System.Collections;
using UnityEngine;

public enum RenderMode
{
    Height,
    Slope,
    Moisture,
    Flow,
    Biome
}

public static class MapRenderer
{
    public static Texture2D Render(WorldData world, RenderMode mode)
    {
        Texture2D texture = new Texture2D(world.Resolution, world.Resolution);

        float min = 0;
        float max = 1;

        if (mode == RenderMode.Height)
        {
            GetMinMax(world.height, out min, out max);
        }
        else if (mode == RenderMode.Slope)
        {
            GetMinMax(world.slope, out min, out max);
        }
        else if (mode == RenderMode.Moisture)
        {
            GetMinMax(world.moisture, out min, out max);
        }
        else if (mode == RenderMode.Flow)
        {
            GetMinMax(world.flow, out min, out max);
        }

        for (int x = 0; x < world.Resolution; x++)
        {
            for (int y = 0; y < world.Resolution; y++)
            {
                Color colour = Color.magenta;

                switch (mode)
                {
                    case RenderMode.Biome:
                        colour = GetBiomeColour(world.biomes[x, y]);
                        break;

                    case RenderMode.Height:
                        colour = Color.Lerp(Color.black, Color.white,
                            Normalise(world.height[x, y], min, max));
                        break;

                    case RenderMode.Slope:
                        colour = Color.Lerp(Color.green, Color.red,
                            Normalise(world.slope[x, y], min, max));
                        break;

                    case RenderMode.Moisture:
                        colour = Color.Lerp(Color.yellow, Color.blue,
                            world.moisture[x, y]);
                        break;

                    case RenderMode.Flow:
                        colour = Color.Lerp(Color.black, Color.cyan,
                            world.flow[x, y]);
                        break;
                }

                texture.SetPixel(x, y, colour);
            }
        }

        texture.Apply();

        return texture;
    }

    private static Color GetBiomeColour(BiomeType biome)
    {
        switch (biome)
        {
            case BiomeType.Ocean: return new Color(0, 0.2f, 0.8f);
            case BiomeType.Beach: return new Color(0.9f, 0.8f, 0.4f);
            case BiomeType.Forest: return new Color(0.1f, 0.6f, 0.1f);
            case BiomeType.Cliff: return new Color(0.4f, 0.4f, 0.4f);
            case BiomeType.Mountain: return Color.white;
            default: return Color.magenta;
        }
    }

    private static float Normalise(float value, float min, float max)
    {
        if (max - min == 0) return 0;
        return Mathf.Clamp01((value - min) / (max - min));
    }

    private static void GetMinMax(float[,] data, out float min, out float max)
    {
        min = float.MaxValue;
        max = float.MinValue;

        int size = data.GetLength(0);

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                float v = data[x, y];

                if (v < min) min = v;
                if (v > max) max = v;
            }
        }
    }
}