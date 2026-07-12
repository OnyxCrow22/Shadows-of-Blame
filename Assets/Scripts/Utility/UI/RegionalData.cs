using UnityEngine;

// A definition for vehicle types
public enum VehicleDef
{
    None,
    Sedan,
    SUV,
    Sports,
    Hyper,
    Van,
    Lorry,
    Utility,
    Emergency,
    Plane,
    Helicopter,
    Boat
}

[RequireComponent(typeof(LineRenderer))]
public class RegionalData : MonoBehaviour
{
    [Header("Geographical Features")] // For roads, cities, regions and districts
    public string regionalArea = ""; // Country or Province
    public string city = ""; // City name
    public string district = ""; // District name
    public string roadName = ""; // Road name

    [Header("Vehicle Features")]
    public string vehicleName = ""; // Name of vehicle
    public VehicleDef spawnVehicleInArea = VehicleDef.None; // Allow vehicles to spawn in particular regions - Set to none by default.

    [Header("GPS Detection")]
    [Tooltip("The distance required for Harrison to display the road name")] // Proximity to the region / city / road
    public float detectionRadius = 5f; // 5 metres

    [HideInInspector] public LineRenderer informationRender; // The component required for this system to work.

    private void Awake()
    {
        informationRender = GetComponent<LineRenderer>();
    }
}
