using UnityEngine;

// Expose to other classes.
public static class TileGenerator
{
    public static float[,] TileHeights(
        int resolution, // Resolution of the terrain
        Vector2 tileWorldOrigin, // The origin point of the tile
        float totalWidthMetres, // The width of the tile
        float totalLengthMetres, // The length of the tile
        float maximumHeight, // The maximum permitted height of the tile
        int gridDimension,
        int seed, // The number for the island seed
        float noiseScale, // For Mountains and Valleys
        int octaves, // How detailed should the noise be?
        float persistence, // How persistent should the octave be?
        float lacunarity,
        bool enablePlateaus, // Should plateaus be enabled?
        float plateauElevation, // Height of the plateau on the tile
        float plateauWidth, // The width of a plateau
        float plateauFlatness, // The flatness of a plateau
        AnimationCurve heightCurve, // Determines the height of the tile
        float islandRadius,
        float edgeSmoothness, // How smooth should the edge be
        float coastWarpScale,     // Frequency of bays & peninsulas
        float coastWarpStrength // Depth of coves and shore irregularity
     )

    {
        float[,] heights = new float[resolution, resolution];

        Random.InitState(seed); // For randomising the seed of the island
        Vector2 seedOffset = new Vector2(Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));

        for (int x = 0; x < resolution; x++) // For loop on the x axis
        {
            for (int z = 0; z < resolution; z++) // For loop on the z axis
            {
                // Finds the continous world coordinates
                float worldX = tileWorldOrigin.x + ((float)x / (resolution - 1)) * (totalWidthMetres / gridDimension);
                float worldZ = tileWorldOrigin.y + ((float)z / (resolution - 1)) * (totalLengthMetres / gridDimension);

                // Applies noise to the terrain
                float rawNoise = EvaluateFBM(worldX + seedOffset.x, worldZ + seedOffset.y, noiseScale, octaves, persistence, lacunarity);

                // Are plateaus allowed?
                if (enablePlateaus)
                {
                    rawNoise = ApplyPlateauFilter(rawNoise, plateauElevation, plateauWidth, plateauFlatness);
                }

                // Remap the noise using the height curve.
                float remappedNoise = heightCurve.Evaluate(rawNoise);

                // Define the coastline of a island.
                float mask = CalculateGlobalIslands(worldX, worldZ, totalWidthMetres, totalLengthMetres, islandRadius, edgeSmoothness, warp);

                heights[z, x] = Mathf.Clamp01(remappedNoise * mask);
            }
        }

        return heights;
    }

    private static float ApplyPlateauFilter(float noiseVal, float elevation, float width, float flatness)
    {
        float plateauMin = elevation - (width * 0.5f);
        float plateauMax = elevation + (width * 0.5f);

        if (noiseVal >= plateauMin && noiseVal <= plateauMax)
        {
            float topography = (noiseVal - plateauMin) / width;
            float flattened = Mathf.Lerp(noiseVal, elevation, flatness);
            return Mathf.SmoothStep(plateauMin, plateauMax, topography) * (1f - flatness) + (flattened * flatness);
        }

        return noiseVal;
    }

    private static float EvaluateFBM(float x, float z, float scale, int octaves, float persistance, float lacunarity)
    {
        float total = 0f;
        float frequency = 1f;
        float amplitude = 1f;
        float maxVal = 0;

        // For loop using the octaves variable
        for (int i = 0;  i < octaves; i++)
        {
            float sampleX = (x / scale) * frequency;
            float sampleZ = (z / scale) * frequency;

            total += Mathf.PerlinNoise(sampleX, sampleZ) * amplitude;
            maxVal += amplitude;

            amplitude *= persistance;
            frequency *= lacunarity;
        }

        return total / maxVal;
    }

    private static float CalculateGlobalIslands(
        float worldX, float worldZ,
        float totalWidth, float totalLength,
        float radius, float smoothness,
        float warpScale, float warpStrength)
    {
        // Remap world position to centered normalized coordinates (-1 to +1)
        float nx = (2f * worldX / totalWidth) - 1f;
        float nz = (2f * worldZ / totalLength) - 1f;

        // 1. Circular/Radial distance from map center (replaces square box)
        float distance = Mathf.Sqrt(nx * nx + nz * nz);

        // 2. Domain Warping: Use noise to distort the distance field, forming natural bays & peninsulas
        float warpNoise = (Mathf.PerlinNoise(nx * warpScale + 50f, nz * warpScale + 50f) - 0.5f) * warpStrength;
        float distortedDistance = distance + warpNoise;

        // 3. Smooth falloff to sea level at the shore
        return 1f - Mathf.SmoothStep(radius - smoothness, radius + smoothness, distortedDistance);
    }
}
