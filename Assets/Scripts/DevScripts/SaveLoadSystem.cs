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
    [SerializeField] public GameData data;

    IDataService service;
    public LoadingScreen load;

    public void Awake()
    {
        service = new FileDataService(new JSONSerialiser());
    }

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoad;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoad;

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FrontEnd" || scene.name == "TestScene") return;

        Player player = FindAnyObjectByType<Player>();

        if (player != null)
        {
            Bind<Player, PlayerData>(data.pData);
        }
        else
        {
            Debug.LogWarning("Unable to find player! Values will be default!");
        }
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
        load.LoadLevel(data.currentLevelName);
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

        if (Time.timeScale == 0 && AudioListener.pause == true)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }

        load.LoadLevel(data.currentLevelName);
    }

    public void ReloadGame() => LoadGame(data.Name);

    public void DeleteGame(string gameName) => service.Delete(gameName);
}
