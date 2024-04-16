using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable] public class GameData
{
    public string Name;
    public string currentLevelName;
    public PlayerData pData;
}

public interface ISavable
{
   public string ID { get; set; }
}

public interface IBind<TData> where TData : ISavable
{
    public string ID { get; }
    void Bind(TData data);
}

public class SaveLoadSystem : MonoBehaviour
{
    public GameData data;

    IDataService service;

    protected void Awake()
    {
        service = new FileDataService(new JSONSerialiser());
    }

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoad;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoad;

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FrontEnd" || scene.name == "TestScene") return;

        Bind<Player, PlayerData>(data.pData);
    }

    void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISavable, new()
    {
        var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
        if (entity != null)
        {
            if (data == null)
            {
                data = new TData { ID = entity.ID };
            }
            entity.Bind(data);
        }
    }

    void Bind<T, TData>(List<TData> datas) where T : MonoBehaviour, IBind<TData> where TData : ISavable, new()
    {
        var entities = FindObjectsByType<T>(FindObjectsSortMode.None);

        foreach(var entity in entities)
        {
            var data = datas.FirstOrDefault(d => d.ID == entity.ID);
            if (data == null)
            {
                data = new TData() { ID = entity.ID };
                datas.Add(data);
            }
            entity.Bind(data);
        }
    }

    public void NewGame()
    {
        data = new GameData
        {
            Name = "New Game",
            currentLevelName = "ShadowsOfBlame",
        };
        SceneManager.LoadScene("ShadowsOfBlame");
    }

    public void SaveGame() => service.Save(data);

    public void LoadGame(string gameName)
    {
        data = service.Load(gameName);
        Debug.Log("LOADING GAME: " + gameName);

        if (string.IsNullOrWhiteSpace(data.currentLevelName))
        {
            data.currentLevelName = "ShadowsOfBlame";
        }

        SceneManager.LoadScene(data.currentLevelName);
    }

    public void ReloadGame() => LoadGame(data.Name);

    public void DeleteGame(string gameName) => service.Delete(gameName);
}
