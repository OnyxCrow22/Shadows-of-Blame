using System.Collections;
using UnityEngine;

public enum BiomeType
{
    Ocean,
    Beach,
    Grasslands,
    Forest,
    Mountain,
    Cliff
}

public class WorldData
{
    public int Resolution;

    public float[,] height;
    public float[,] slope;
    public float[,] moisture;
    public float[,] flow;
    public Vector2[,] flowDir;

    public BiomeType[,] biomes;

    public WorldData(int resolution)
    {
        Resolution = resolution;

        height = new float[resolution, resolution];
        slope = new float[resolution, resolution];
        moisture = new float[resolution, resolution];
        flow = new float[resolution, resolution];
        flowDir = new Vector2[resolution, resolution];

        biomes = new BiomeType[resolution, resolution];
    }
}