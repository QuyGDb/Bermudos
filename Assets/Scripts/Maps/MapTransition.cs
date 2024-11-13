
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapTransition : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI text;
    private bool isLoadedMap;
    public float transitonTime = 0.25f;

    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        image.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StaticEventHandler.OnMapTransition += OnMapTransition;
    }

    private void OnDestroy()
    {
        StaticEventHandler.OnMapTransition += OnMapTransition;
    }

    private void OnMapTransition()
    {
        image.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        Sequence transition = DOTween.Sequence();
        transition.Append(image.DOFade(1, transitonTime / 2).SetEase(Ease.InOutSine)).Join(text.DOFade(1, transitonTime / 2).SetEase(Ease.InOutSine));

        transition.Append(image.DOFade(0, transitonTime / 2)).Join(text.DOFade(0, transitonTime / 2).SetEase(Ease.InOutSine)).AppendCallback(() =>
        {
            image.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        });

    }
}
