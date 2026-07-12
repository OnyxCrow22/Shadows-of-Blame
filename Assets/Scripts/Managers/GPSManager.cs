using System.Collections.Generic;
using UnityEngine;

public class GPSManager : MonoBehaviour
{
    public Transform player;
    private List<RegionalData> regions = new List<RegionalData>();

    public void RegisterRegion(RegionalData region)
    {
        regions.Add(region);
    }

    private void Update()
    {
        RegionalData closest = null;
        float closestDist = Mathf.Infinity;

        foreach (var region in regions)
        {
            float dist = Vector3.Distance(player.position, region.transform.position);
            if (dist < region.detectionRadius && dist < closestDist)
            {
                closestDist = dist;
                closest = region;
            }
        }

        if (closest != null)
        {
            // Tell Harrison UI:
            // city, district, roadName, regionalArea
        }
    }
}
