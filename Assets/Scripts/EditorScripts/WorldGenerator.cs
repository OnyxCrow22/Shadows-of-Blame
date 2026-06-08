using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public WorldControlMap worldControl;
    public BiomeDatabase bDatabase;
    public GameObject terrainPrefab;

    [ContextMenu("Print World Information")]
    public void printInformation()
    {
        Debug.Log($"World size: {WorldSettings.worldWidth} x {WorldSettings.worldHeight} meters");
        Debug.Log($"Chunks: {WorldSettings.chunkX} x {WorldSettings.chunkY}");
        Debug.Log($"Total chunks: {WorldSettings.chunkX * WorldSettings.chunkY}");
    }
    
    [ContextMenu("Test Control Map Samples")]
    public void printTestSample()
    {
        Vector3 testPosition = new Vector3(1000, 0, 5000);
        Color c = worldControl.sampleWorld(testPosition.x, testPosition.z);
        Debug.Log($"Sampled colour at {testPosition}: {c}");
    }

    [ContextMenu("Test biome system")]
    public void printBiome()
    {
        Vector3 testPosition = new Vector3(1000, 0, 5000);
        Color c = worldControl.sampleWorld(testPosition.x, testPosition.z);

        BiomeRules rule = bDatabase.GetBiomeByColour(c);

        if (rule == null)
            Debug.Log("No biome assoicated with this colour");
        else
            Debug.Log($"Biome found: {rule.name}");
    }

    [ContextMenu("Generate World")]
    public void GenerateWorld()
    {
        for (int x = 0; x < WorldSettings.chunkX; x++)
        {
            for (int y = 0; y < WorldSettings.chunkY; y++)
            {
                GameObject chunk = Instantiate(terrainPrefab);
                chunk.transform.position = WorldSettings.WorldChunk(x, y);

                TerrainChunk tc = chunk.GetComponent<TerrainChunk>();
                tc.chunkX = x;
                tc.chunkY = y;
                tc.worldControl = worldControl;
                tc.bDatabase = bDatabase;

                Color c = worldControl.sampleWorld(
                    x * WorldSettings.chunkSize, 
                    y * WorldSettings.chunkSize
                );

                Color bColour = worldControl.sampleWorld(x, y);
                BiomeRules rule = bDatabase.GetBiomeByColour(bColour);

                tc.Generate();
            }
        }
    }
}
