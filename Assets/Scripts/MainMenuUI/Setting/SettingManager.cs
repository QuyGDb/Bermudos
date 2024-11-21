using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SettingManager : MonoBehaviour
{
    [SerializeField] private Button controlsBtn;
    [SerializeField] private Button audioBtn;
    [SerializeField] private Button backBtn;
    private TextMeshProUGUI controlsText;
    private TextMeshProUGUI audioText;
    [SerializeField] private Image controlsPanel;
    [SerializeField] private Image audioPanel;
    private float aplha;
    private float startFontSize;
    private void Awake()
    {
        controlsText = controlsBtn.GetComponentInChildren<TextMeshProUGUI>();
        audioText = audioBtn.GetComponentInChildren<TextMeshProUGUI>();

    }
    private void Start()
    {
        controlsBtn.onClick.AddListener(OpenControls);
        audioBtn.onClick.AddListener(OpenAudio);
        backBtn.onClick.AddListener(() =>
        {
            SoundEffectManager.Instance.PlaySoundEffect(GameResources.Instance.clickButton);
            gameObject.SetActive(false);
        });
        aplha = 0.5f;
        startFontSize = 10f;
    }
    private void OnDestroy()
    {
        DOTween.Kill(this.transform);
    }
    private void OpenControls()
    {
        audioPanel.gameObject.SetActive(false);
        controlsPanel.gameObject.SetActive(true);
        SoundEffectManager.Instance.PlaySoundEffect(GameResources.Instance.clickButton);
        Effect(controlsText, audioText);
    }
    private void OpenAudio()
    {
        controlsPanel.gameObject.SetActive(false);
        audioPanel.gameObject.SetActive(true);
        SoundEffectManager.Instance.PlaySoundEffect(GameResources.Instance.clickButton);
        Effect(audioText, controlsText);
    }
    private void Effect(TextMeshProUGUI currentText, TextMeshProUGUI previousText)
    {
        previousText.fontSize = startFontSize;
        previousText.color = new Color(audioText.color.r, audioText.color.g, audioText.color.b, aplha);
        float startSize = currentText.fontSize;
        float targetSize = 12.5f;
        var temp = DOTween.KillAll();
        Debug.Log(temp);
        currentText.DOFade(1f, 0.5f).SetEase(Ease.InBounce);
        DOTween.To(() => startSize, x =>
        {
            startSize = x;
            currentText.fontSize = startSize;
        }, targetSize, 0.5f);
    }
    #region Validation
#if UNITY_EDITOR 
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(controlsBtn), controlsBtn);
        HelperUtilities.ValidateCheckNullValue(this, nameof(audioBtn), audioBtn);
        HelperUtilities.ValidateCheckNullValue(this, nameof(backBtn), backBtn);
        HelperUtilities.ValidateCheckNullValue(this, nameof(controlsPanel), controlsPanel);
        HelperUtilities.ValidateCheckNullValue(this, nameof(audioPanel), audioPanel);
    }
#endif
    #endregion
}
