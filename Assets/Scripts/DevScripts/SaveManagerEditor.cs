using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveLoadSystem))]
public class SaveManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SaveLoadSystem slSystem = (SaveLoadSystem) target;
        string gameName = slSystem.data.Name;

        DrawDefaultInspector();

        if (GUILayout.Button("Save Game"))
        {
            slSystem.SaveGame();
        }

        if (GUILayout.Button("Load Game"))
        {
            slSystem.LoadGame(gameName);
        }

        if (GUILayout.Button("Delete Game"))
        {
            slSystem.DeleteGame(gameName);
        }
    }
}
