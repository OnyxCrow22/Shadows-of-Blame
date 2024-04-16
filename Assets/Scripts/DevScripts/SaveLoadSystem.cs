using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable] public class GameData
{
    public string Name;
    public string currentLevelName;
    public PlayerMovementSM playerData;
}

public interface ISavable
{
    string ID { get; }
}

public interface IBind<TData> where TData : ISavable
{
    string ID { get; set; }
    void Bind(TData data);
}

public class SaveLoadSystem : MonoBehaviour
{
    [SerializeField] public GameData data;

    [SerializeField] public IDataService service;

    protected void Awake()
    {
        service = new FileDataService(new JSONSerialiser());
    }

    public void NewGame()
    {
        data = new GameData
        {
            Name = "New Game",
            currentLevelName = "ShadowsOfBlame"
        };
        SceneManager.LoadScene(data.currentLevelName);
    }

    public void SaveGame()
    {
        service.Save(data);
    }

    public void LoadGame(string gameName)
    {
        data = service.Load(gameName);

        if (String.IsNullOrWhiteSpace(data.currentLevelName))
        {
            data.currentLevelName = "ShadowsOfBlame";
        }

        SceneManager.LoadScene(data.currentLevelName);
    }

    public void ReloadGame() => LoadGame(data.Name);

    public void DeleteGame(string gameName) => service.Delete(gameName);
}
