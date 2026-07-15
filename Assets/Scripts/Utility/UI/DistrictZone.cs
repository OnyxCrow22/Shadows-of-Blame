using System;
using System.Collections.Generic;
using UnityEngine;

public class DistrictZone : MonoBehaviour
{
    public RegionData regionData; // Reference to the RegionData ScriptableObject
    public List<Collider> districtColliders; // List of colliders representing districts
    public UIController UI;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Vector3 playerPos = player.transform.position;

            foreach(Collider collider in districtColliders)
        {
            if (collider == null)
            {
                Debug.LogWarning("A district collider is not assigned in the inspector.");
            }

            if (collider.bounds.Contains(playerPos))
            {
                UI.DisplayRegion(regionData);
                Debug.Log($"Player has entered the district zone: {regionData.cityName}");
                break; // Exit the loop once the player is found in a district
            }
        }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.DisplayRegion(regionData);
            Debug.Log($"Player has entered the district zone: {regionData.cityName}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.ClearRegionDisplay();
            Debug.Log($"Player has exited the district zone: {regionData.cityName}");
        }
    }
}