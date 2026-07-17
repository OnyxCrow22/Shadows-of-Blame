using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float positionX, positionY, positionZ;
    public float playerHealth;
    public int activeSceneIndex;

    public void GetData(Vector3 position, float health)
    {
        positionX = position.x;
        positionY = position.y;
        positionZ = position.z;

        playerHealth = health;
    }

    public Vector3 ConvertPosition()
    {
        return new Vector3(positionX, positionY, positionZ);
    }
}
