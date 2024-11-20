using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject NorthOfTheForestToTheForest;
    [SerializeField] private SoundEffectSO waweEffect;
    [SerializeField] private bool isHasWater;
    private float repeatInterval = 10f;
    private void Start()
    {
        StaticEventHandler.CallMapChangedEvent(this);
        if (isHasWater && waweEffect != null)
        {
            InvokeRepeating(nameof(PlayWaveSoundEffect), 0f, repeatInterval);
        }
    }
    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += OnGameStateChanged_Map;
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(PlayWaveSoundEffect));
        GameManager.Instance.OnGameStateChange -= OnGameStateChanged_Map;
    }
    private void PlayWaveSoundEffect()
    {
        SoundEffectManager.Instance.PlaySoundEffect(waweEffect);
    }
    private void OnGameStateChanged_Map(GameState gameState)
    {

        if (gameState == GameState.EngagedBoss && NorthOfTheForestToTheForest != null)
        {
            NorthOfTheForestToTheForest.SetActive(false);
        }
        if (gameState == GameState.Won)
        {
            NorthOfTheForestToTheForest.SetActive(true);
        }
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(NorthOfTheForestToTheForest), NorthOfTheForestToTheForest);
        HelperUtilities.ValidateCheckNullValue(this, nameof(waweEffect), waweEffect);
    }
#endif
    #endregion
}
