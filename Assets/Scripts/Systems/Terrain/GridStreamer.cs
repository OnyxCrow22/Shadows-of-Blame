using UnityEngine;

public class GridStreamer : MonoBehaviour
{
    [Header("Grid / World Configuration")]
    public Transform playerTransform; // Location of the player
    public Material terrainMaterial; // Material for the terrain
    public int gridDimension = 4; // 4x4 for the grid
    public float totalMapSize; // Set dynamically, not statically.
    public float maximumHeight; // Not 600f by default
    public int heightMapResolution; // No value = Able to change at will
    public float loadDistanceThreshold; // No value = change at will

    [Header("Procedural Generation Parameters")]
    public int seed; // Do not specify seed, leave it random
    public float noiseScale; // Do not specify, leave random
    public int octaves; // Randomly set
    [Range(0f, 1f)] public float persistence;
    public float lucnarity;

    [Header("Plateau Configuration")]
    public bool enablePlateaus;
    [Range(0.2f, 0.8f)] public float plateauElevation;
    [Range(0.01f, 0.3f)] public float plateauWidth;
    [Range(0f, 1f)] public float plateauFlatness;

    [Header("Architecture Setup")]
    public AnimationCurve heightMapRemap = AnimationCurve.Linear(0, 0, 1, 1);

    private Terrain[,] terrainGridded;
    private Vector3[,] tileCentre;
    private float tileLength;
    private float loadDistanceSquared;

    [Header("Coastline Shape & Distortion")]
    [Range(0.1f, 0.9f)] public float islandRadius = 0.45f;
    [Range(0.01f, 0.5f)] public float edgeSmoothness = 0.25f;
    [Range(1f, 10f)] public float coastWarpScale = 3.5f;     // Frequency of bays & peninsulas
    [Range(0f, 0.8f)] public float coastWarpStrength = 0.35f; // Depth of coves and shore irregularit

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileLength = totalMapSize / gridDimension;
        loadDistanceSquared = loadDistanceThreshold * loadDistanceThreshold;

        terrainGridded = new Terrain[gridDimension, gridDimension];
        tileCentre = new Vector3[gridDimension, gridDimension];

        ConstructGrid();
        SewGridNeighbours();
    }

    // Constructs the terrain into a series of grids, optimising performance.
    private void ConstructGrid()
    {
        // For loop for the x axis
        for (int x = 0; x < gridDimension; x++)
        {
            // For loop on the z axis
            for (int z = 0; z < gridDimension; z++)
            {
                Vector2 worldOrigin = new Vector2(x * tileLength, z * tileLength);

                tileCentre[x, z] = new Vector3(worldOrigin.x + (tileLength * 0.5f), 0f, worldOrigin.y + (tileLength * 0.5f));

                TerrainData terrainInformation = new TerrainData
                {
                    heightmapResolution = heightMapResolution,
                    size = new Vector3(tileLength, maximumHeight, tileLength)
                };

                float[,] heights = TileGenerator.TileHeights(
                    heightMapResolution, worldOrigin, totalMapSize, totalMapSize, maximumHeight,
                    gridDimension, seed, noiseScale, octaves, persistence, lucnarity,
                    enablePlateaus, plateauElevation, plateauWidth, plateauFlatness,
                    heightMapRemap, islandRadius, edgeSmoothness, coastWarpScale, coastWarpStrength
                );

                terrainInformation.SetHeights(0, 0, heights);

                // Create the Terrain GameObject
                GameObject newTerrain = Terrain.CreateTerrainGameObject(terrainInformation);
                newTerrain.name = $"Island_Chunk_{x}_{z}";
                newTerrain.transform.position = new Vector3(worldOrigin.x, 0f, worldOrigin.y);
                newTerrain.transform.parent = transform;

                Terrain fetchTerrain = newTerrain.GetComponent<Terrain>();
                if (terrainMaterial != null) fetchTerrain.materialTemplate = terrainMaterial;

                terrainGridded[x, z] = fetchTerrain;

                newTerrain.SetActive(false);
            }
        }
    }

    private void SewGridNeighbours()
    {
        // For loop on the x axis
        for (int x = 0; x < gridDimension; x++)
        {
            // for loop on the z axis
            for (int z = 0; z < gridDimension; z++)
            {
                Terrain leftSide = (x > 0) ? terrainGridded[x - 1, z] : null;
                Terrain rightSide = (x < gridDimension - 1) ? terrainGridded[x + 1, z] : null;
                Terrain surface = (z > 0) ? terrainGridded[x, z - 1] : null;
                Terrain sky = (z < gridDimension - 1) ? terrainGridded[x, z + 1] : null;

                terrainGridded[x, z].SetNeighbors(leftSide, sky, rightSide, surface);
            }
        }
    }

    public void GenerateIslandEditor()
    {
        ClearTerrain();

        // Re-initialize core grid dimensions
        if (noiseScale <= 0f) noiseScale = 1000f;
        if (gridDimension <= 0) gridDimension = 4;

        tileLength = totalMapSize / gridDimension;
        terrainGridded = new Terrain[gridDimension, gridDimension];
        tileCentre = new Vector3[gridDimension, gridDimension];

        ConstructGrid();
        SewGridNeighbours();

        // Keep all chunks visible in Scene View while editing
        for (int x = 0; x < gridDimension; x++)
        {
            for (int z = 0; z < gridDimension; z++)
            {
                if (terrainGridded[x, z] != null)
                {
                    terrainGridded[x, z].gameObject.SetActive(true);
                }
            }
        }
    }

    public void ClearTerrain()
    {
        // In Edit Mode, we MUST use DestroyImmediate
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null) return;

        Vector3 playerPos = playerTransform.position;

        // Check all pre-calculated tile centres using two for loops
        for (int x = 0; x < gridDimension; x++)
        {
            for (int z = 0; z < gridDimension; z++)
            {
                Terrain newTile = terrainGridded[x, z];
                float squaredDistance = (playerPos - tileCentre[x, z]).sqrMagnitude;

                bool shouldBeActive = squaredDistance <= loadDistanceSquared;

                if (newTile.gameObject.activeSelf != shouldBeActive)
                {
                    newTile.gameObject.SetActive(shouldBeActive);
                }
            }
        }
    }
}
