using Esper.ESave;
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
    [SerializeField] private Intro intro;
    private SaveFileSetup saveFileSetup;
    bool isfirstTime = true;
    private void Awake()
    {
        saveFileSetup = GetComponent<SaveFileSetup>();
    }
    private void Start()
    {
        if (saveFileSetup.GetSaveFile().HasData("FirstTime"))
        {
            saveFileSetup.GetSaveFile().AddOrUpdateData("FirstTime", false);
            saveFileSetup.GetSaveFile().Save();
            isfirstTime = false;
        }
        else
        {
            saveFileSetup.GetSaveFile().AddOrUpdateData("FirstTime", true);
            saveFileSetup.GetSaveFile().Save();
            isfirstTime = true;
        }
        newGameBtn.onClick.AddListener(OnNewGameClick);
        continueGameBtn.onClick.AddListener(OnContinueGameClick);
        settingsBtn.onClick.AddListener(OnSettingsClick);
        exitGameBtn.onClick.AddListener(OnExitGameClick);

    }
    private void OnNewGameClick()
    {
        SoundEffectManager.Instance.PlaySoundEffect(GameResources.Instance.clickButton);
        intro.gameObject.SetActive(true);
    }
    private void OnContinueGameClick()
    {
        if (isfirstTime) return;
        SceneManager.LoadScene("MainScene");
        SceneManager.sceneLoaded += LoadContineGameScene;
        SoundEffectManager.Instance.PlaySoundEffect(GameResources.Instance.clickButton);
    }

    private void LoadContineGameScene(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "MainScene" && scene.isLoaded)
        {
            GameManager.Instance.TransitionToGame();
            SceneManager.sceneLoaded -= LoadContineGameScene;
        }
    }
    private void OnSettingsClick()
    {
        SettingPanel.gameObject.SetActive(true);
        controlsPanel.gameObject.SetActive(false);
        audioPanel.gameObject.SetActive(false);
        SoundEffectManager.Instance.PlaySoundEffect(GameResources.Instance.clickButton);
    }
    private void OnExitGameClick()
    {
        SoundEffectManager.Instance.PlaySoundEffect(GameResources.Instance.clickButton);
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
