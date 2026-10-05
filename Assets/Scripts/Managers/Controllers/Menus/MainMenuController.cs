using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{

    private PanelRenderer mainMenuRenderer;
    private VisualElement root;
    private VisualElement mainMenuRoot;
    private VisualElement settingsRoot;
    private Button newGameBtn;
    private Button quitBtn;
    private Button settingsBtn;
    private int loadedVersion = -1;
    private void OnEnable()
    {
        mainMenuRenderer = GetComponent<PanelRenderer>();
        mainMenuRenderer.RegisterUIReloadCallback(OnUIReload); 
    }

    private void OnDisable()
    {
        mainMenuRenderer.UnregisterUIReloadCallback(OnUIReload);
        UnsubscribeEvents();
    }

    private void OnUIReload(PanelRenderer render, VisualElement currentRoot, int version)
    {
        if (version == loadedVersion) return;
        loadedVersion = version;
        root = currentRoot;

        newGameBtn = root.Q<Button>("newgame-btn");
        quitBtn = root.Q<Button>("application-quit-btn");
        settingsBtn = root.Q<Button>("settings-btn");

        mainMenuRoot = root.Q<VisualElement>("main-menu-root");
        settingsRoot = root.Q<VisualElement>("settings-root");
        
        settingsRoot?.AddToClassList("hidden");
        mainMenuRoot?.RemoveFromClassList("hidden");

        var backBtn = root.Q<Button>("back-btn");
        if (backBtn != null) backBtn.clicked += OnBackSettingsClicked;

        newGameBtn.clicked += OnNewGameClicked;
        quitBtn.clicked += OnQuitClicked;
        settingsBtn.clicked += OnSettingsClicked;
    }

    private void UnsubscribeEvents()
    {
        newGameBtn.clicked -= OnNewGameClicked;
        quitBtn.clicked -= OnQuitClicked;
        settingsBtn.clicked -= OnSettingsClicked;
    }
    private void OnNewGameClicked()
    {
        // Load the main game scene
        // Note to self: Write a more sopshisticated scene management system later, this is just a quick and dirty solution for now.
        SceneManager.LoadScene("ShadowsOfBlame");
    }

    private void OnSettingsClicked()
    {
        settingsRoot.RemoveFromClassList("hidden");
        mainMenuRoot.AddToClassList("hidden");
    }

    private void OnBackSettingsClicked()
    {
        settingsRoot.AddToClassList("hidden");
        mainMenuRoot.RemoveFromClassList("hidden");
    }

    private void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
