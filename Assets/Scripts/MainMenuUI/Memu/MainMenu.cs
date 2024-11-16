using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameBtn;
    [SerializeField] private Button continueGameBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button exitGameBtn;
    [SerializeField] private Image SettingPanel;
    [SerializeField] private Image controlsPanel;
    [SerializeField] private Image audioPanel;
    private Intro intro;

    private void Awake()
    {
        intro = GetComponentInChildren<Intro>();
    }
    private void Start()
    {
        newGameBtn.onClick.AddListener(OnNewGameClick);
        continueGameBtn.onClick.AddListener(OnContinueGameClick);
        settingsBtn.onClick.AddListener(OnSettingsClick);
        exitGameBtn.onClick.AddListener(OnExitGameClick);
        intro.gameObject.SetActive(false);
    }
    private void OnNewGameClick()
    {
        intro.gameObject.SetActive(true);
    }
    private void OnContinueGameClick()
    {
        SceneManager.LoadScene("MainScene");
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
        {
            if (scene.name == "MainScene" && scene.isLoaded)
            {
                GameManager.Instance.HandleGameState(GameState.Play);
            }
        };
    }
    private void OnSettingsClick()
    {
        SettingPanel.gameObject.SetActive(true);
        controlsPanel.gameObject.SetActive(false);
        audioPanel.gameObject.SetActive(false);
    }
    private void OnExitGameClick()
    {
        Application.Quit();
    }


    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(newGameBtn), newGameBtn);
        HelperUtilities.ValidateCheckNullValue(this, nameof(continueGameBtn), continueGameBtn);
        HelperUtilities.ValidateCheckNullValue(this, nameof(settingsBtn), settingsBtn);
        HelperUtilities.ValidateCheckNullValue(this, nameof(exitGameBtn), exitGameBtn);
        HelperUtilities.ValidateCheckNullValue(this, nameof(SettingPanel), SettingPanel);
        HelperUtilities.ValidateCheckNullValue(this, nameof(controlsPanel), controlsPanel);
        HelperUtilities.ValidateCheckNullValue(this, nameof(audioPanel), audioPanel);
    }
#endif
    #endregion
}
