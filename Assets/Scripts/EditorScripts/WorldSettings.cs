using UnityEngine;

public static class WorldSettings
{
    // The world size in metres
    public const int worldWidth = 24000; // Equivalent to 24 kilometres
    public const int worldHeight = 18000; // Equivalent to 18 kilometres

    public const int maxTerrainHeight = 1500;

    // Split the world into 512x512 chunks
    public const int chunkSize = 512; // Each chunk is 512 x 512 (so whole towns could be in one chunk)

    // Deriviative values
    public static int chunkX => worldWidth / chunkSize;
    public static int chunkY => worldHeight / chunkSize;

    // Convert the world position into chunk indexes
    public static Vector2Int ChunkWorld(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / chunkSize); // Get the world position on the x-coordinate, and divide it by the size of the chunk.
        int z = Mathf.FloorToInt(worldPosition.z / chunkSize); // Get the world position on the z-coordinate, and divide it by the size of the chunk.
        return new Vector2Int(x, z); // Combine the two values together to create a new chunk. 
    }

    // Convert the newly created chunk index back into the world position
    public static Vector3 WorldChunk(int chunkX, int chunkZ)
    {
        return new Vector3(chunkX * chunkSize, 0, chunkZ * chunkSize); // Return the value of the x and y chunks.
    }
}
