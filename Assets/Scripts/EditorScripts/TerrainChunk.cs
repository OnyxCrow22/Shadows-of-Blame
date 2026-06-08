using UnityEngine;

public class TerrainChunk : MonoBehaviour
{
    public WorldControlMap worldControl;
    public BiomeDatabase bDatabase;
    public int chunkX;
    public int chunkY;

    public void Generate()
    {
        Debug.Log($"Generating chunk ({chunkX}, {chunkY}) with worldControl: {worldControl != null}");
        Debug.Log($"Terrain component found: {GetComponent<Terrain>() != null}");
        Terrain terrainInfo = GetComponent<Terrain>();

        terrainInfo.terrainData = Instantiate(terrainInfo.terrainData);

        TerrainData terrainData = terrainInfo.terrainData;

        int resolution = terrainData.heightmapResolution;
        float[,] heights = new float[resolution, resolution];

        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float percentX = (float)x / (resolution - 1);
                float percentY = (float)y / (resolution - 1);

                float worldX = (chunkX * WorldSettings.chunkSize) + (percentX * WorldSettings.chunkSize);
                float worldZ = (chunkY * WorldSettings.chunkSize) + (percentY * WorldSettings.chunkSize);
                

                Color bColour = worldControl.sampleWorld(worldX, worldZ);
                BiomeRules rule = bDatabase.GetBiomeByColour(bColour);
                
                float totalNoise = 0f;
                float noise = 1f;
                float amptitude = rule.noiseAmptitude;
                float frequency = rule.noiseFrequency;
                float maxAmptitude = 0;

                for (int o = 0; o < 5; o++)
                {
                    float nx = worldX / 1000f;
                    float nz = worldZ / 1000f;

                    totalNoise += Mathf.PerlinNoise (nx, nz) * amptitude;

                    maxAmptitude += amptitude;
                    amptitude *= 0.5f;
                    frequency *= 2;
                }

                float normalisedNoise = totalNoise / maxAmptitude;
                float curved = rule.height.Evaluate(normalisedNoise);

                float height = rule.baseHeight + (curved * rule.noiseAmptitude);

                heights[y, x] = Mathf.Clamp01(height / WorldSettings.maxTerrainHeight);

                if (x == 0 && y == 0)

                {
                    Debug.Log($"Noise: {noise}, Curved: {curved}, Height: {height}");
                }

            }
        }

        terrainData.SetHeights(0, 0, heights);

        Debug.Log("TerrainData instance ID: " + terrainData.GetEntityId());
    }
}
