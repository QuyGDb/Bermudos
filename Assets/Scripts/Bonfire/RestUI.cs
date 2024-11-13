
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestUI : MonoBehaviour
{
    private Image restImage;
    private TextMeshProUGUI restText;

    private void Awake()
    {
        restText = GetComponentInChildren<TextMeshProUGUI>();
        restImage = GetComponent<Image>();
    }
    private void Start()
    {
        restImage.gameObject.SetActive(false);
        restText.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StaticEventHandler.OnRestInBonfire += ShowRestUI;
    }
    private void OnDestroy()
    {
        StaticEventHandler.OnRestInBonfire -= ShowRestUI;
    }
    private void ShowRestUI()
    {
        restImage.gameObject.SetActive(true);
        restText.gameObject.SetActive(true);
        Sequence restSequence = DOTween.Sequence();

        restSequence.Append(restText.DOFade(1f, 1f).SetEase(Ease.InOutSine))
                  .Join(restImage.DOFade(0.6f, 1f).SetEase(Ease.InOutSine));

        restSequence.Append(restText.DOFade(0f, 1f).SetEase(Ease.InOutSine))
                  .Join(restImage.DOFade(0f, 1f).SetEase(Ease.InOutSine)).AppendCallback(() =>
                  {
                      restImage.gameObject.SetActive(false);
                      restText.gameObject.SetActive(false);
                  });
    }

}
