using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{

    [SerializeField] RectTransform backgroundrectTransform;
    [SerializeField] GameObject bossUI;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossContainer;

    private void Start()
    {
        backgroundrectTransform.DOSizeDelta(new Vector2(2200, 600), 1f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            bossUI.transform.DOLocalMove(new Vector2(50, -11), 1).SetEase(Ease.OutBack).OnComplete(() =>
            {
                backgroundrectTransform.DOSizeDelta(new Vector2(2200, 2000), 1f).SetEase(Ease.InBack);
                bossUI.transform.DOLocalMove(new Vector2(1820, -387), 1).SetEase(Ease.InBack).OnComplete(() =>
                {
                    SpawnBoss();
                    gameObject.SetActive(false);
                });

            });
        });
    }

    void SpawnBoss()
    {

        Instantiate(boss, new Vector3(14, 15, 0), Quaternion.identity, bossContainer.transform);
    }


    private void OnDestroy()
    {
        DOTween.Kill(backgroundrectTransform);
    }
    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(backgroundrectTransform), backgroundrectTransform);
        HelperUtilities.ValidateCheckNullValue(this, nameof(bossUI), bossUI);
        HelperUtilities.ValidateCheckNullValue(this, nameof(boss), boss);
        HelperUtilities.ValidateCheckNullValue(this, nameof(bossContainer), bossContainer);
    }
#endif
    #endregion
}
