using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMap : Map
{
    [SerializeField] private GameObject NorthOfTheForestToTheForest;

    private void Start()
    {
        StaticEventHandler.CallMapChangedEvent(this);
    }
    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += OnGameStateChanged_Map;
    }

    private void OnDisable()
    {

        GameManager.Instance.OnGameStateChange -= OnGameStateChanged_Map;
    }
    private void OnGameStateChanged_Map(GameState gameState)
    {

        if (gameState == GameState.EngagedBoss && NorthOfTheForestToTheForest != null)
        {
            if (GetBossState() == 1)
                NorthOfTheForestToTheForest.SetActive(true);
            else
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
    }
#endif
    #endregion
}
