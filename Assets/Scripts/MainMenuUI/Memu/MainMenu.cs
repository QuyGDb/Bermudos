using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameBtn;
    [SerializeField] private Button continueGameBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button exitGameBtn;
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
        Debug.Log("Continue Game");
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
    }
    private void OnExitGameClick()
    {
        Application.Quit();
    }

}
