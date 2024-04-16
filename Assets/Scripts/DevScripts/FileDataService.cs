using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class FileDataService : IDataService
{
    ISerialiser serialiser;
    string dataPath;
    string fileExtension;

    public FileDataService(ISerialiser serialiser)
    {
        this.dataPath = Application.persistentDataPath;
        this.fileExtension = "json";
        this.serialiser = serialiser;
    }

    public string GetPathToFile(string filename)
    {
        return Path.Combine(dataPath, string.Concat(filename, ".", fileExtension));
    }

    public void Save(GameData data, bool overwrite = true)
    {
        string fileLocation = GetPathToFile(data.Name);

        if (!overwrite && File.Exists(fileLocation))
        {
            throw new IOException($"The file '{data.Name}.{fileLocation}' already exists at this location. It cannot be overwritten");
        }

        File.WriteAllText(fileLocation, serialiser.Seralize(data));
    }

    public GameData Load(string name)
    {
        string fileLocation = GetPathToFile(name);

        if(!File.Exists(fileLocation))
        {
            throw new ArgumentException($"No game data found with name'{name}'");
        }

        return serialiser.DeSerialize<GameData>(File.ReadAllText(fileLocation));
    }

    public void Delete(string name)
    {
        string fileLocation = GetPathToFile(name);

        if (File.Exists(fileLocation))
        {
            File.Delete(fileLocation);
        }
    }

    public void DeleteAll()
    {
        foreach (string filePath in Directory.GetFiles(dataPath))
        {
            File.Delete(filePath);
        }
    }

    public IEnumerable<string> ListSaves()
    {
        foreach (string path in Directory.EnumerateFiles(dataPath))
        {
            if (Path.GetExtension(path) == fileExtension)
            {
                yield return Path.GetFileNameWithoutExtension(path);
            }
        }
    }
}
