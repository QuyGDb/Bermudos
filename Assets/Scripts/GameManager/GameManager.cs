using DG.Tweening;
using Esper.ESave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [HideInInspector] public Player player;
    public EnemyManagerDetailsSO[] enemyManagerDetailsSO;
    [HideInInspector] public SaveFileSetup saveFileSetup;
    public GameState gameState;
    [HideInInspector] public Action<GameState> OnGameStateChange;
    [SerializeField] Image beginSreen;
    private void OnEnable()
    {
        StaticEventHandler.OnPlayerChanged += StaticEventHandler_OnPlayerChanged;
        StaticEventHandler.OnRestInBonfire += StaticEventHandler_OnRestInBonfire;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnPlayerChanged -= StaticEventHandler_OnPlayerChanged;
        StaticEventHandler.OnRestInBonfire -= StaticEventHandler_OnRestInBonfire;
    }

    private void StaticEventHandler_OnPlayerChanged(OnPlayerChangedEventArgs onPlayerChangedEventArgs)
    {
        this.player = onPlayerChangedEventArgs.player;
    }

    private void StaticEventHandler_OnRestInBonfire()
    {
        InitializeEnemyManagerData();
    }
    override protected void Awake()
    {
        base.Awake();
        // SceneManager.LoadScene("Coast", LoadSceneMode.Additive);
        saveFileSetup = GetComponent<SaveFileSetup>();

    }
    public void HandleGameState(GameState gameState)
    {
        this.gameState = gameState;
        switch (gameState)
        {

            case GameState.Begin:
                Begin();
                break;
            case GameState.Instruct:
                break;
            case GameState.Play:
                Play();
                break;
            case GameState.EngagedBoss:
                EngagedBoss();
                break;
            case GameState.Won:
                break;
            default:
                break;
        }
        OnGameStateChange?.Invoke(gameState);
    }

    public void Begin()
    {
        beginSreen.gameObject.SetActive(true);
        beginSreen.DOColor(new Color(0, 0, 0, 1), 3f).OnComplete(() =>
        {
            SceneManager.LoadScene("Coast", LoadSceneMode.Additive);
            beginSreen.DOFillAmount(0, 1.5f).OnComplete(() =>
            {
                beginSreen.gameObject.SetActive(false);

                HandleGameState(GameState.Instruct);
            });
        });
    }
    public void Play()
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.mass = 0.5f;

    }
    public void EngagedBoss()
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.mass = 1000000f;
    }
    private void Start()
    {
        InitializeEnemyManagerData();
        SceneManager.LoadScene("Coast", LoadSceneMode.Additive);
        HandleGameState(GameState.Instruct);
    }

    public void InitializeEnemyManagerData()
    {
        foreach (var enemyManagerDetails in enemyManagerDetailsSO)
        {
            EnemyManagerData enemyManagerData = new EnemyManagerData(enemyManagerDetails.enemyName, enemyManagerDetails.enemyPostionList.Count, -1);
            saveFileSetup.GetSaveFile().AddOrUpdateData(enemyManagerDetails.enemyManagerDataKey, enemyManagerData);
            saveFileSetup.GetSaveFile().Save();
        }
    }

}
