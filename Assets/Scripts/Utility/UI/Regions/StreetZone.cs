using System.Collections.Generic;
using UnityEngine;

public class StreetZone : MonoBehaviour
{
    public int currentStreetIndex; // Index of the street associated with this zone
    private int activeColliderCount;
    public RegionData regionData; // Reference to the RegionData ScriptableObject
    public List<Collider> streetColliders = new List<Collider>(); // List of colliders representing the street zone

    public UIController UI; // Reference to the UIController for displaying region information

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Vector3 playerPos = player.transform.position;

            foreach(Collider streetCol in streetColliders)
        {
            if (streetCol == null)
            {
                Debug.LogWarning("A street collider is not assigned in the inspector.");
            }

            if (streetCol.bounds.Contains(playerPos))
            {
                if (regionData != null && currentStreetIndex >= 0 && currentStreetIndex < regionData.streetNames.Count)
                {
                    string activeStreet = regionData.streetNames[currentStreetIndex];
                    UI.DisplayRegion(regionData, activeStreet);
                    Debug.Log($"Player has entered the street zone: {activeStreet}");
                    break; // Exit the loop once the player is found in a street
                }
                else
                {
                    Debug.LogWarning("RegionData is not assigned or currentStreetIndex is out of bounds.");
                }

            }
        }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activeColliderCount++;

            if (activeColliderCount == 1)
            {
                if (regionData != null && currentStreetIndex >= 0 && currentStreetIndex < regionData.streetNames.Count)
                {
                    string activeStreet = regionData.streetNames[currentStreetIndex];
                    UI.DisplayRegion(regionData, activeStreet);
                    Debug.Log($"Player has entered the street zone: {activeStreet}");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activeColliderCount--;

            if (activeColliderCount <= 0)
            {
                activeColliderCount = 0; // Ensure the count doesn't go below 0
                UI.ClearRegionDisplay();
                Debug.Log($"Player has exited the street zone: {regionData.cityName}");
            }
        }
    }
}