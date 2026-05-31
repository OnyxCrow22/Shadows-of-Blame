using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [Header("Menu Items")]
    public GameObject loadingScreen;
    public GameObject mainMenu;
    [Header("Loading functionality")]
    public Slider loadingBar;
    public TextMeshProUGUI tipsText;
    public TextAsset tips;

    public void LoadLevel(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        // Load method
        StartCoroutine(LoadAsync(levelToLoad));
    }

    IEnumerator LoadAsync(string levelToLoad)
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOp.isDone)
        {
            float progressVal = Mathf.Clamp01(loadOp.progress / 0.9f);
            loadingBar.value = progressVal;
            SetRandomInformationTip();
            yield return null;
            yield return new WaitForSeconds(2);
        }
    }

    void SetRandomInformationTip()
    {
        string[] tipsLines = tips.text.Split("\n");
        int infoRand = Random.Range(0, tipsLines.Length);
        tipsText.text = tipsLines[infoRand];
    }
}
