using System.Collections;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    [SerializeField] int resolution = 512;

    [SerializeField] Renderer targetRenderer;

    [SerializeField] RenderMode mode;

    private WorldData world;
    private WorldDebugControls wDebug;

    private void Awake()
    {
        wDebug = new WorldDebugControls();

        wDebug.DebuggingActions.HeightViewing.performed += _ => SetMode(RenderMode.Height);
        wDebug.DebuggingActions.HeightViewing.performed += _ => Debug.Log("HEIGHT REGISTERED");
        wDebug.DebuggingActions.SlopeViewing.performed += _ => SetMode(RenderMode.Slope);
        wDebug.DebuggingActions.MoistureView.performed += _ => SetMode(RenderMode.Moisture);
        wDebug.DebuggingActions.FlowView.performed += _ => SetMode(RenderMode.Flow);
        wDebug.DebuggingActions.BiomeView.performed += _ => SetMode(RenderMode.Biome);

        wDebug.DebuggingActions.Regenerate.performed += _ => RegenerateWorld();
    }

    private void Update()
    {
        Debug.Log("Updating");
    }

    void OnEnable() => wDebug.Enable();
    private void OnDisable() => wDebug.Disable();

    private void Start()
    {
        RegenerateWorld();
    }

    void SetMode(RenderMode newMode)
    {
        mode = newMode;
        RenderMap();
    }

    void RegenerateWorld()
    {
        world = new WorldData(resolution);

        HeightGenerator.Generate(world);

        for (int i = 0; i < 10; i++)
        {
            FlowMapGenerator.Generate(world);
            TerrainAnalysis.CalculateSlope(world);
            HydrualicErosion.Erode(world);
            ThermalErosion.Erode(world);
        }

        TerrainAnalysis.CalculateSlope(world);
        MoistureGenerator.Generate(world);
        RiverGenerator.Generate(world);
        RiverCarver.Carve(world);
        BiomeGenerator.Generate(world);

        RenderMap();
    }

    void RenderMap()
    {
        Texture2D map = MapRenderer.Render(world, mode);
        targetRenderer.material.mainTexture = map;
    }
}