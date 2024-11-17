
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPEffects.Components;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private IntroSO[] introSOList = new IntroSO[4];
    [SerializeField] private Image introImage;
    [SerializeField] private Image map;
    private TextMeshProUGUI introText;
    private Button nextBtn;
    private int introIndex = 0;
    TMPWriter tmpWriter;

    private void Awake()
    {
        tmpWriter = GetComponentInChildren<TMPWriter>();
        introText = GetComponentInChildren<TextMeshProUGUI>();
        nextBtn = GetComponentInChildren<Button>();
        nextBtn.gameObject.SetActive(false);
        introImage.enabled = false;
    }

    private void OnDestroy()
    {
        tmpWriter.OnFinishWriter.RemoveListener(OnFinishWriter);
        nextBtn.onClick.RemoveListener(OnNextBtnClick);
        DOTween.Kill(this.transform);
    }
    private void Start()
    {

        map.gameObject.SetActive(true);
        map.gameObject.transform.DOScale(new Vector3(8f, 8f, 8f), 2.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            map.gameObject.SetActive(false);
            tmpWriter.OnFinishWriter.AddListener(OnFinishWriter);
            StartCoroutine(WaitUntilIntroEnable());
            nextBtn.onClick.AddListener(OnNextBtnClick);
            introImage.enabled = true;
        });

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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        introIndex++;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene" && scene.isLoaded)
        {
            GameManager.Instance.HandleGameState(GameState.Intro);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
    private IEnumerator WaitUntilIntroEnable()
    {
        yield return new WaitForSeconds(2);
        introImage.color = new Color(1, 1, 1, 1f);
        OnNextBtnClick();
    }
}
