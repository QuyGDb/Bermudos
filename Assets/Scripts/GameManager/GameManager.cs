using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [HideInInspector] public Player player;

    private void OnEnable()
    {
        StaticEventHandler.OnPlayerChanged += StaticEventHandler_OnPlayerChanged;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnPlayerChanged -= StaticEventHandler_OnPlayerChanged;
    }

    private void StaticEventHandler_OnPlayerChanged(OnPlayerChangedEventArgs onPlayerChangedEventArgs)
    {
        this.player = onPlayerChangedEventArgs.player;
    }
    override protected void Awake()
    {
        base.Awake();
        SceneManager.LoadScene("Coast", LoadSceneMode.Additive);
    }

}
