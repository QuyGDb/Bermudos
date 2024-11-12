using Esper.ESave;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [HideInInspector] public Player player;
    public EnemyManagerDetailsSO[] enemyManagerDetailsSO;
    [HideInInspector] public SaveFileSetup saveFileSetup;

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
        SceneManager.LoadScene("Coast", LoadSceneMode.Additive);
        saveFileSetup = GetComponent<SaveFileSetup>();

    }
    private void Start()
    {
        InitializeEnemyManagerData();
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
