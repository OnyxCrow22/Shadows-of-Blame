using UnityEngine;

public class WorldControlMap : MonoBehaviour
{
    [Header("World layout")]
    public Texture2D worldControlMap;

    public Color sampleWorld(float worldX, float worldZ)
    {
        float u = Mathf.InverseLerp(0, WorldSettings.worldWidth, worldX);
        float z = Mathf.InverseLerp(0, WorldSettings.worldHeight, worldZ);

        int px = Mathf.FloorToInt(u * worldControlMap.width);
        int py = Mathf.FloorToInt(z * worldControlMap.height);

        return worldControlMap.GetPixel(px, py);
    }
}
