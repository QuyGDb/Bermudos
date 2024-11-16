using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNameMovement : MonoBehaviour
{
    private RectTransform rectTransform;
    public float moveDistance = 5f;
    public float duration = 1f;
    public Ease easeType = Ease.InOutSine;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        GameNameAnimation();
    }
    private void OnDestroy()
    {
        DOTween.Kill(this.transform);
    }
    void GameNameAnimation()
    {
        Vector3 startPosition = rectTransform.anchoredPosition;
        Vector3 topPosition = startPosition + new Vector3(0, moveDistance, 0);
        Vector3 bottomPosition = startPosition - new Vector3(0, moveDistance, 0);
        rectTransform.DOAnchorPosY(topPosition.y, duration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                rectTransform.DOAnchorPosY(bottomPosition.y, duration)
                     .SetEase(easeType)
                     .SetLoops(-1, LoopType.Yoyo);

            });
    }
}
