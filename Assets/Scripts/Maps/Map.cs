using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
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
            Debug.Log("EngagedBoss");
            NorthOfTheForestToTheForest.SetActive(false);
        }
        if (gameState == GameState.Won)
        {
            NorthOfTheForestToTheForest.SetActive(true);
        }

    }
}
