using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridStreamer))]
public class GridStreamerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw standard fields
        DrawDefaultInspector();

        GridStreamer streamer = (GridStreamer)target;

        EditorGUILayout.Space(15);
        GUI.backgroundColor = new Color(0.3f, 0.8f, 0.3f);
        if (GUILayout.Button("Generate Island in Scene", GUILayout.Height(35)))
        {
            streamer.GenerateIslandEditor();
        }

        GUI.backgroundColor = new Color(0.9f, 0.3f, 0.3f);
        if (GUILayout.Button("Clear Island", GUILayout.Height(25)))
        {
            streamer.ClearTerrain();
        }
        GUI.backgroundColor = Color.white;
    }
}