using UnityEngine;

public class SaveSystem
{
    private const string SaveFileName = "harrison_save.json";

    public static void SavePlayer(Vector3 position, float health)
    {
        // Create a new save file.
        PlayerData data = new PlayerData();

        data.GetData(position, health);
        string json = JsonUtility.ToJson(data, true);

        System.IO.File.WriteAllText(GetSave(), json);
    }

    public static string GetSave()
    {
        return Application.persistentDataPath + "/" + SaveFileName;
    }

    public static PlayerData LoadPlayer()
    {
        string path = GetSave();

        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            return data;
        }

        return null;
    }
}