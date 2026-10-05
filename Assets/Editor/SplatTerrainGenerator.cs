using UnityEngine;
using UnityEditor;

public class SplatTerrainGenerator : EditorWindow
{
    [Header("Terrain Settings")]
    public GameObject terrainGroup; // The core holder of the island / islands
    public Texture2D 
        landSilhouetteMap, // Black and white image of the target island 
        topographyMap, // Target height ranges for the target island
        splatControlMap; // Biome control for the island

    Vector2 scrollPosition; // Track the current position of the scrollbar

    [Header("Oceanic settings")]
    public float 
        seaLevelHeight, // The base line for the island
        oceanFloorDepth; // How deep will the ocean be?

    [Header("Mountains & Hills Settings")]
    public float 
        foothillLiftHeight,
        maximumMountainHeight,
        mountainNoiseScale,
        mountainPeakSharpness,
        maximumHillHeight,
        hillNoiseScale;

    public int mountainOctaves;

    [Header("Rivers and Lakes Settings")]
    public float
        maximumRiverDepth,
        targetReservoirHeight,
        maximumLakeDepth;

    public bool isReservoir = false;

    [Header("Height control")]
    public Vector2
        coastalHeightRange = new(2.5f, 5f), // How high above sea level is the coast
        lowlandPlainsHeight = new(6f, 30f), // How high are the lowlands
        inlandPlainsHeight = new(31f, 90f), // How high are the inland plains
        capitalPlateauHeight = new(91f, 140f), // How high is the capital plateau
        cliffDropHeight = new(100f, 155f); // How sharp the cliff drop is.

    [Header("Random and custom seeds")]
    public bool useRandomSeed; // Are we using random seeds?
    public int customIslandSeed = 2453; // Generate a island based on the seed.

    [Header("Topography control")]
    public int topographySmoothPass = 2;
    public float smoothIntensity = 0.75f;

    [MenuItem("Tools/ShadowsOfBlame")]
    public static void ShowWindow()
    {
        GetWindow<SplatTerrainGenerator>("Custom Island Generation System (CIGS)");
    }

    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition); // Start scrolling

        GUILayout.Label("Island Textures and Objects", EditorStyles.boldLabel); // Set up the label for the GameObject and Texture2D objects.
        terrainGroup = (GameObject)EditorGUILayout.ObjectField("Assign Terrain Group:", terrainGroup, typeof(GameObject), true); // Must pass it through this, otherwise it will not work.
        landSilhouetteMap = (Texture2D)EditorGUILayout.ObjectField("Assign Black and White Image: ", landSilhouetteMap, typeof(Texture2D), false);
        topographyMap = (Texture2D)EditorGUILayout.ObjectField("Assign Topography Map: ", topographyMap, typeof(Texture2D), false);
        splatControlMap = (Texture2D)EditorGUILayout.ObjectField("Assign Height Control: ", splatControlMap, typeof(Texture2D), false);

        EditorGUILayout.Space(); // Seperate features out neatly.

        GUILayout.Label("Control Sea Level and Ocean Depth", EditorStyles.boldLabel);
        seaLevelHeight = EditorGUILayout.FloatField("Set Global Sea Level Height: ", seaLevelHeight);
        oceanFloorDepth = EditorGUILayout.FloatField("Set Global Ocean Depth: ", oceanFloorDepth);

        EditorGUILayout.Space();
        GUILayout.Label("Set Mountain and Hills Heights", EditorStyles.boldLabel);
        foothillLiftHeight = EditorGUILayout.FloatField("Set Foothill Lift: ", foothillLiftHeight);
        maximumMountainHeight = EditorGUILayout.FloatField("Set Maximum Mountain Height: ", maximumMountainHeight);
        mountainNoiseScale = EditorGUILayout.FloatField("Set Mountain Noise Scale: ", mountainNoiseScale);
        mountainPeakSharpness = EditorGUILayout.FloatField("Set Mountain Peak Sharpness: ", mountainPeakSharpness);
        mountainOctaves = EditorGUILayout.IntField("Set Mountain Octaves: ", mountainOctaves);
        maximumHillHeight = EditorGUILayout.FloatField("Set Maximum Hill Height: ", maximumHillHeight);
        hillNoiseScale = EditorGUILayout.FloatField("Set Hill Noise Scale: ", hillNoiseScale);

        EditorGUILayout.Space();

        GUILayout.Label("Control Rivers and Lakes", EditorStyles.boldLabel);
        maximumRiverDepth = EditorGUILayout.FloatField("Set River Depth ", maximumRiverDepth);
        maximumLakeDepth = EditorGUILayout.FloatField("Set Lake Depth ", maximumLakeDepth);
        targetReservoirHeight = EditorGUILayout.FloatField("Set Target Reservoir Height ", targetReservoirHeight);
        isReservoir = EditorGUILayout.Toggle("Is this a reservoir?", isReservoir);

        EditorGUILayout.Space();

        GUILayout.Label("Set Biome heights and topography control", EditorStyles.boldLabel);
        coastalHeightRange = EditorGUILayout.Vector2Field("Set Coastal Biome Height: ", coastalHeightRange);
        lowlandPlainsHeight = EditorGUILayout.Vector2Field("Set Lowlands Plain Biome Height: ", lowlandPlainsHeight);
        inlandPlainsHeight = EditorGUILayout.Vector2Field("Set Inland Plains Biome Height: ", inlandPlainsHeight);
        capitalPlateauHeight = EditorGUILayout.Vector2Field("Set the Capital Plateau Height: ", capitalPlateauHeight);
        cliffDropHeight = EditorGUILayout.Vector2Field("Set Cliff Drop Height: ", cliffDropHeight);

        EditorGUILayout.Space();

        GUILayout.Label("Random seed / Custom seed", EditorStyles.boldLabel);
        useRandomSeed = EditorGUILayout.Toggle("Enable Random Seed? ", useRandomSeed);

        if (!useRandomSeed)
            customIslandSeed = EditorGUILayout.IntField("Set Custom Island Seed", customIslandSeed);

        EditorGUILayout.Space();
        if (GUILayout.Button("Assign Values to Target Island"))
        {
            GenerateTerrain();
        }

        EditorGUILayout.EndScrollView(); // Stop scrolling
    }

    public void GenerateTerrain()
    {
        // Assigned all variables?
        if (terrainGroup == null || splatControlMap == null || landSilhouetteMap == null || topographyMap == null)
        {
            EditorUtility.DisplayDialog("ERROR", $"Operation cannot proceed as {terrainGroup}, {landSilhouetteMap}, {splatControlMap}, {topographyMap} values are not assigned", "OK");
            return;
        }

        Terrain[] terrainSections = terrainGroup.GetComponentsInChildren<Terrain>();
        // Is there at least one section?
        if (terrainSections.Length == 0)
        {
            EditorUtility.DisplayDialog("ERROR", $"Operation failed because {terrainSections} has no sections. Please ensure {terrainSections} has sections.", "OK");
            return;
        }

        int currentActiveSeed = useRandomSeed ? Random.Range(1, 999999) : customIslandSeed; // Set the seed at random.
        Random.InitState(currentActiveSeed); // Assign the random seed.

        float generateCliffHeight = Random.Range(cliffDropHeight.x, cliffDropHeight.y);
        float generateCoastHeight = Random.Range(coastalHeightRange.x, coastalHeightRange.y);
        float generateLowlandsHeight = Random.Range(lowlandPlainsHeight.x, lowlandPlainsHeight.y);
        float generateInlandHeight = Random.Range(inlandPlainsHeight.x, inlandPlainsHeight.y);
        float generateCapitalPlateauHeight = Random.Range(capitalPlateauHeight.x, capitalPlateauHeight.y);

        // Find the minimum and maximum bounds of each terrain.
        Vector3 minBounds = new(float.MaxValue, 0, float.MaxValue);
        Vector3 maxBounds = new(float.MinValue, 0, float.MinValue);

        foreach (Terrain tSection in terrainSections)
        {
            Vector3 tilePosition = tSection.transform.position; // Get the position of the terrain section.
            Vector3 tileSize = tSection.terrainData.size; // Get the size of the tile.

            minBounds.x = Mathf.Min(minBounds.x, tilePosition.x);
            minBounds.z = Mathf.Min(minBounds.z, tilePosition.z);

            maxBounds.x = Mathf.Max(maxBounds.x, tilePosition.x + tileSize.x);
            maxBounds.z = Mathf.Max(maxBounds.z, tilePosition.z + tileSize.z);
        }

        float totalWidth = maxBounds.x - minBounds.x;
        float totalLength = maxBounds.z - minBounds.z;

        foreach (Terrain tSection in terrainSections)
        {
            TerrainData terrainTile = tSection.terrainData;

            int tileResolution = terrainTile.heightmapResolution;

            float[,] tileHeights = new float[tileResolution, tileResolution];

            Vector3 tilePosition = tSection.transform.position;
            Vector3 tileSize = tSection.terrainData.size;

            Undo.RegisterCompleteObjectUndo(terrainTile, "Assign Target Values to Island");

            for (int y = 0; y < tileResolution; y++)
            {
                for (int x = 0; x < tileResolution; x++)
                {
                    if (tileHeights[y, x] <= 0f) continue; // Cannot perform operation.

                    float localXPosition = (float)x / (tileResolution - 1);
                    float localZPosition = (float)y / (tileResolution - 1);

                    float worldXPosition = tilePosition.x + (localXPosition * tileSize.x);
                    float worldZPosition = tilePosition.z + (localZPosition * tileSize.z);

                    float globalUCoordinate = (worldXPosition - minBounds.x) / totalWidth;
                    float globalVCoordinate = (worldZPosition - minBounds.z) / totalLength;

                    float landAlpha = landSilhouetteMap.GetPixelBilinear(globalUCoordinate, globalVCoordinate).r; // Sample the red pixel using the global coordinates
                    Color topographyPixel = topographyMap.GetPixelBilinear(globalUCoordinate, globalVCoordinate);

                    float r = topographyPixel.r; // Red
                    float g = topographyPixel.g; // Green
                    float b = topographyPixel.b; // Blue

                    float brightness = (r + g + b) / 3f;
                    float RGBVariance = Mathf.Abs(r - g) + Mathf.Abs(g - b) + Mathf.Abs(r - b); // Find the RGB difference

                    bool isCliff = (brightness > 0.15f && brightness < 0.45f) && (RGBVariance < 0.12f);

                    float coastalWeight = Mathf.Clamp01(r * g * (1f - b)); // Get Yellow
                    float lowLandsWeight = Mathf.Clamp01(g * r * 1.5f); // Get Light Green
                    float inlandsWeight = Mathf.Clamp01(g * (1f - r)); // Get Dark Green
                    float capitalPlateauWeight = Mathf.Clamp01(r * (1f - g)); // Get Brown

                    float colorSum = coastalWeight + lowLandsWeight + inlandsWeight + capitalPlateauWeight;

                    float targetElevation;

                    if (isCliff) // Override elevation values
                    {
                        targetElevation = generateCliffHeight;
                    }
                    else if (colorSum > 0.001f)
                    {
                        float blendedHeight =
                            (coastalWeight * generateCoastHeight) +
                            (lowLandsWeight * generateLowlandsHeight) +
                            (inlandsWeight * generateInlandHeight) +
                            (capitalPlateauWeight * generateCapitalPlateauHeight);

                        targetElevation = blendedHeight / colorSum;
                    }
                    else
                    {
                        targetElevation = coastalHeightRange.x;
                    }

                    if (landAlpha <= 0.001f)
                    {
                        // Set seabed depth
                        tileHeights[y, x] = Mathf.Max(0f, oceanFloorDepth) / tileSize.y;
                    }
                    else
                    {
                        float coastalRamp = isCliff ? 1f : Mathf.SmoothStep(0f, 1f, landAlpha);
                        float calculatedElevation = Mathf.Lerp(oceanFloorDepth, targetElevation, coastalRamp);

                        tileHeights[y, x] = Mathf.Clamp01(calculatedElevation /  tileSize.y);
                    }
                }
            }

            SmoothHeightMap(tileHeights, tileResolution, topographySmoothPass, smoothIntensity);
            terrainTile.SetHeights(0, 0, tileHeights);
            terrainTile.SyncHeightmap();
            EditorUtility.SetDirty(terrainTile);
        }
    }

    private void SmoothHeightMap(float[,] heights, int resolution, int passes, float blendStrength)
    {
        float[,] temporaryHeights = new float[resolution, resolution]; // Set a temporary height

        for (int p = 0; p < passes; p++)
        {
            System.Array.Copy(heights, temporaryHeights, heights.Length); // Copy the Array

            for (int y = 1; y < resolution - 1; y++)
            {
                for (int x = 1; x < resolution - 1; x++)
                {
                    float neighbourSum = 0f;
                    neighbourSum += temporaryHeights[y - 1, x - 1] + temporaryHeights[y - 1, x] + temporaryHeights[y - 1, x + 1];
                    neighbourSum += temporaryHeights[y,     x - 1] + temporaryHeights[y,     x] + temporaryHeights[y,     x + 1];
                    neighbourSum += temporaryHeights[y + 1, x - 1] + temporaryHeights[y + 1, x] + temporaryHeights[y + 1, x + 1];

                    float averageHeight = neighbourSum / 9f;

                    heights[y, x] = Mathf.Lerp(heights[y, x], averageHeight, blendStrength);
                }
            }
        }
    }

    public float CalculateFractualNoise(float x, float y, float scale, int octaves, float persistance, float lacurnaity)
    {
        float total = 0f;
        float frequency = scale;
        float amplitude = 1f;
        float maxValue = 0f;

        for (int i = 0; i < octaves; i++)
        {
            total += Mathf.PerlinNoise(x * frequency, y * frequency) * amplitude;
            maxValue += amplitude;
            amplitude *= persistance;
            frequency *= lacurnaity;
        }

        return total / maxValue;
    }

    public float CalculateRidgedNoise(float x, float y, float scale, int octaves, float persistance, float lacurnaity)
    {
        float warpScale = scale * 0.5f;
        float warpX = (Mathf.PerlinNoise(x * warpScale + 12.3f, y * warpScale + 45.6f) * 2f - 1f) * 150f;
        float warpY = (Mathf.PerlinNoise(x * warpScale + 78.9f, y * warpScale + 10.1f) * 2f - 1f) * 150f;

        float warpedX = x * warpX;
        float warpedY = y * warpY;

        float total = 0f;
        float frequency = scale;
        float amplitude = 1f;
        float maxValue = 0f;
        float weight = 1f;

        for (int i = 0; i < octaves; i++)
        {
            float n = Mathf.PerlinNoise(warpedX * frequency, warpedY * frequency) * 2f - 1f;
            n = 1f - Mathf.Abs(n);
            n *= n;

            n *= weight;
            weight = Mathf.Clamp01(n * 2f);

            total += n * amplitude;
            maxValue = amplitude;
            amplitude *= persistance;
            frequency *= lacurnaity;
        }

        return total / maxValue;
    }
}