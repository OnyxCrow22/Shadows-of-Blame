using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public TextMeshProUGUI tipsText;
    public TextAsset tips;

    private string[] tipsLines;

    private void Awake()
    {
        tipsLines = tips.text.Split("\n");
    }

    private void OnEnable()
    {
        SceneLoader.OnLoadStarted += ShowScreen;
        SceneLoader.OnLoadProgress += UpdateProgress;
        SceneLoader.OnLoadFinished += HideScreen;
    }

    private void OnDisable()
    {
        SceneLoader.OnLoadStarted -= ShowScreen;
        SceneLoader.OnLoadProgress -= UpdateProgress;
        SceneLoader.OnLoadFinished -= HideScreen;
    }

    private void ShowScreen()
    {
        loadingScreen.SetActive(true);
        SetRandomTip();
    }

    private void UpdateProgress(float value)
    {
        loadingBar.value = value;
    }

    private void HideScreen()
    {
        loadingScreen.SetActive(false);
    }

    private void SetRandomTip()
    {
        int index = Random.Range(0, tipsLines.Length);
        tipsText.text = tipsLines[index];
    }
}
