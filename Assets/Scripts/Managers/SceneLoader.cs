using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour
{
    public static event Action<float> OnLoadProgress;
    public static event Action OnLoadStarted;
    public static event Action OnLoadFinished;

    public static void LoadScene(string sceneName)
    {
        OnLoadStarted?.Invoke();
        Instance.StartCoroutine(LoadAsync(sceneName));
    }

    private static SceneLoader Instance;

    private void Awake()
    {
        Instance = this;
    }

    private static IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            OnLoadProgress?.Invoke(progress);
            yield return null;
        }

        OnLoadFinished?.Invoke();
    }
}
