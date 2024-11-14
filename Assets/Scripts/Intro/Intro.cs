
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPEffects.Components;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private IntroSO[] introSOList = new IntroSO[4];
    [SerializeField] private Image introImage;
    private TextMeshProUGUI introText;
    private Button nextBtn;
    private int introIndex = 0;
    TMPWriter tmpWriter;

    private void Awake()
    {
        tmpWriter = GetComponentInChildren<TMPWriter>();
        introText = GetComponentInChildren<TextMeshProUGUI>();
        nextBtn = GetComponentInChildren<Button>();
    }
    private void OnEnable()
    {
        tmpWriter.OnFinishWriter.AddListener(OnFinishWriter);
    }
    private void OnDestroy()
    {
        tmpWriter.OnFinishWriter.RemoveListener(OnFinishWriter);
        nextBtn.onClick.RemoveListener(OnNextBtnClick);
    }
    private void Start()
    {
        nextBtn.onClick.AddListener(OnNextBtnClick);
        OnNextBtnClick();
    }
    void OnFinishWriter(TMPWriter tmpWriter)
    {
        nextBtn.gameObject.SetActive(true);
    }

    void OnNextBtnClick()
    {
        introImage.color = new Color(1, 1, 1, 0);
        nextBtn.gameObject.SetActive(false);
        if (introIndex < introSOList.Length)
        {
            introImage.sprite = introSOList[introIndex].introSprite;
            introImage?.DOFade(1, 2f);
            introText.text = introSOList[introIndex].introText;
        }
        else
        {

            SceneManager.LoadScene("MainScene");
            SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
            {
                if (scene.name == "MainScene" && scene.isLoaded)
                {
                    GameManager.Instance.gameState = GameState.Begin;
                    GameManager.Instance.HandleGameState(GameManager.Instance.gameState);
                }
            };
        }
        introIndex++;
    }
}
