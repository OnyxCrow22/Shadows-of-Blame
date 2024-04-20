using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class Menus : MonoBehaviour
{
    [MenuItem("ShadowsOfBlame/Utility/ClearPlayerPrefs")]
    public static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("ALL PLAYER PREFS DELETED");
    }

    [MenuItem("ShadowsOfBlame/Utility/LoadScenes/Test")]
    public static void AccessTest()
    {
        EditorSceneManager.OpenScene("TestScene");
    }
    [MenuItem("ShadowsOfBlame/Utility/LoadScenes/SOB")]
    public static void AccessSOB()
    {
        EditorSceneManager.OpenScene("ShadowsOfBlame");
    }
    [MenuItem("ShadowsOfBlame/Utility/LoadScenes/Front End")]
    public static void AccessFrontEnd()
    {
        EditorSceneManager.OpenScene("FrontEnd");
    }
}
