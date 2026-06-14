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
    public bool[,] rivers;
    public float[,] riverStrength;
    public Vector2Int[,] flowDir;

    public BiomeType[,] biomes;

    public WorldData(int resolution)
    {
        Resolution = resolution;

        height = new float[resolution, resolution];
        slope = new float[resolution, resolution];
        moisture = new float[resolution, resolution];
        flow = new float[resolution, resolution];
        flowDir = new Vector2Int[resolution, resolution];

        rivers = new bool[resolution, resolution];
        riverStrength = new float[resolution, resolution];

        biomes = new BiomeType[resolution, resolution];
    }
}