#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class MeasureTool : EditorWindow
{
    [MenuItem("Tools/Measure Distance Pro")]
    static void Init() { GetWindow<MeasureTool>("Measure Pro"); }

    Vector3 p1, p2;
    bool isPlacing = false; // true = p2 follows mouse
    bool hasMeasurement = false;
    
    bool showKm = true;
    bool showMiles = true;
    bool showMeters = true;
    bool showFeet = false;

    const float M_TO_KM = 0.001f;
    const float M_TO_MILES = 0.000621371f;
    const float M_TO_FEET = 3.28084f;

    void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    void OnGUI()
    {
        GUILayout.Label("Ctrl+Click to start point", EditorStyles.boldLabel);
        GUILayout.Label("Move mouse to preview, Esc to lock", EditorStyles.helpBox);
        GUILayout.Label("Ctrl+Click again to reset", EditorStyles.miniLabel);
        
        GUILayout.Space(5);
        GUILayout.Label("Display Units:", EditorStyles.boldLabel);
        showKm = EditorGUILayout.Toggle("Kilometers", showKm);
        showMiles = EditorGUILayout.Toggle("Miles", showMiles);
        showMeters = EditorGUILayout.Toggle("Meters", showMeters);
        showFeet = EditorGUILayout.Toggle("Feet", showFeet);

        GUILayout.Space(10);
        if (hasMeasurement || isPlacing)
        {
            float distM = Vector3.Distance(p1, p2);
            GUILayout.Label(isPlacing ? "Measuring..." : "Locked:", EditorStyles.boldLabel);
            if (showMeters) GUILayout.Label($"{distM:F1} m");
            if (showKm) GUILayout.Label($"{distM * M_TO_KM:F3} km");
            if (showMiles) GUILayout.Label($"{distM * M_TO_MILES:F3} mi");
            if (showFeet) GUILayout.Label($"{distM * M_TO_FEET:F1} ft");
            
            if (!isPlacing && GUILayout.Button("Copy KM to Clipboard"))
            {
                EditorGUIUtility.systemCopyBuffer = $"{distM * M_TO_KM:F3}";
            }
        }
        else
        {
            GUILayout.Label("Ctrl+Click to begin");
        }

        GUILayout.Space(5);
        if (GUILayout.Button("Clear")) 
        { 
            hasMeasurement = false;
            isPlacing = false;
            SceneView.RepaintAll();
        }
    }

    void OnSceneGUI(SceneView sv)
    {
        Event e = Event.current;

        // Ctrl+Click = start new measurement
        if (e.type == EventType.MouseDown && e.button == 0 && e.control)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                p1 = hit.point;
                p2 = hit.point;
                isPlacing = true;
                hasMeasurement = false;
                e.Use();
                Repaint();
            }
        }

        // Update p2 with mouse while placing
        if (isPlacing && e.type == EventType.MouseMove)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                p2 = hit.point;
                sv.Repaint();
            }
        }

        // Esc = lock measurement
        if (isPlacing && e.type == EventType.KeyDown && e.keyCode == KeyCode.Escape)
        {
            isPlacing = false;
            hasMeasurement = true;
            e.Use();
            Repaint();
        }

        // Draw
        if (isPlacing || hasMeasurement)
        {
            Handles.color = isPlacing ? Color.yellow : Color.cyan;
            Handles.DrawLine(p1, p2);
            
            Handles.color = isPlacing ? Color.yellow : Color.green;
            float size1 = HandleUtility.GetHandleSize(p1) * 0.1f;
            float size2 = HandleUtility.GetHandleSize(p2) * 0.1f;
            Handles.SphereHandleCap(0, p1, Quaternion.identity, size1, EventType.Repaint);
            Handles.SphereHandleCap(0, p2, Quaternion.identity, size2, EventType.Repaint);

            // Label
            float distM = Vector3.Distance(p1, p2);
            string label = isPlacing ? "LIVE:\n" : "";
            if (showMeters) label += $"{distM:F1}m\n";
            if (showKm) label += $"{distM * M_TO_KM:F3}km\n";
            if (showMiles) label += $"{distM * M_TO_MILES:F3}mi\n";
            if (showFeet) label += $"{distM * M_TO_FEET:F1}ft\n";
            
            Vector3 labelPos = p2 + Vector3.up * HandleUtility.GetHandleSize(p2) * 0.5f;
            Handles.Label(labelPos, label.TrimEnd('\n'), EditorStyles.whiteBoldLabel);
        }
        
        if (isPlacing) sv.Repaint();
    }
}
#endif