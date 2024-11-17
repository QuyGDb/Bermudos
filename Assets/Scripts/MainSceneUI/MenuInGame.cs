using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPEffects.Components;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInGame : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button resumeButton;
    private TextMeshProUGUI exitText;
    private TextMeshProUGUI resumeText;
    private TMPWriter exitTMPWriter;
    private TMPWriter resumeTMPWriter;
    private InputSystem_Actions inputSystem_Actions;

    public Ease ease;
    private void Awake()
    {
        exitText = exitButton.GetComponentInChildren<TextMeshProUGUI>();
        resumeText = resumeButton.GetComponentInChildren<TextMeshProUGUI>();
        exitTMPWriter = exitText.GetComponent<TMPWriter>();
        resumeTMPWriter = resumeText.GetComponent<TMPWriter>();
        inputSystem_Actions = new InputSystem_Actions();
        inputSystem_Actions.UI.MenuInGame.Enable();

        inputSystem_Actions.UI.MenuInGame.performed += ctx => OpenMenu();
    }
    private void OnDestroy()
    {
        inputSystem_Actions.UI.MenuInGame.Disable();
        inputSystem_Actions.UI.MenuInGame.performed -= ctx => OpenMenu();
        DOTween.Kill(this.transform);
    }

    private void OpenMenu()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            exitTMPWriter.SetText("<+char>Exit");
            resumeTMPWriter.SetText("<+char>Resume");
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 0.5f).SetEase(ease).SetUpdate(true);
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-Screen.width, 0);
            rectTransform.DOAnchorPos(Vector2.zero, 0.5f).SetEase(ease).SetUpdate(true);
        }

    }

    private void Start()
    {

        exitButton.onClick.AddListener(Exit);
        resumeButton.onClick.AddListener(ResumeGame);
        gameObject.SetActive(false);
    }
    private void Exit()
    {
        StaticEventHandler.CallExitEvent();
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
        exitButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        StaticEventHandler.CallMapTransitionEvent();

        StartCoroutine(WaitMapTransitionEvent());
    }
    IEnumerator WaitMapTransitionEvent()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("MainMenu");
    }
    private void ResumeGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(exitButton), exitButton);
        HelperUtilities.ValidateCheckNullValue(this, nameof(resumeButton), resumeButton);
    }
#endif
    #endregion
}
