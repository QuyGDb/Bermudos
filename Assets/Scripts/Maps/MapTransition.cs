
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class MapTransition : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI text;
    private bool isLoadedMap;
    public float transitonTime = 0.5f;

    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        StaticEventHandler.OnMapTransition += OnMapTransition;
    }
    private void Start()
    {
        image.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }


    private void OnDestroy()
    {
        DOTween.Kill(this.transform);
        StaticEventHandler.OnMapTransition -= OnMapTransition;
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
