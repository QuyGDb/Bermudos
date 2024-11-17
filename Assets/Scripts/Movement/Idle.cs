using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IdleEvent))]
[DisallowMultipleComponent]
public class Idle : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private IdleEvent idleEvent;

    private void Awake()
    {
        // Load components
        rigidBody2D = GetComponent<Rigidbody2D>();
        idleEvent = GetComponent<IdleEvent>();

    }

    private void OnEnable()
    {
        // Subscribe to idle event
        idleEvent.OnIdle += IdleEvent_OnIdle;
        GameManager.Instance.OnGameStateChange += GameStateChanged_OnIdle;
    }

    private void OnDisable()
    {
        // Subscribe to idle event
        idleEvent.OnIdle -= IdleEvent_OnIdle;
    }

    private void GameStateChanged_OnIdle(GameState gameState)
    {
        if (gameState == GameState.Intro)
        {
            rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (gameState == GameState.Instruct)
        {
            rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            GameManager.Instance.OnGameStateChange -= GameStateChanged_OnIdle;
        }
    }
    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        IdleRigidBody();
    }

    /// <summary>
    /// Move the rigidbody component
    /// </summary>
    private void IdleRigidBody()
    {
        // ensure the rb collision detection is set to continuous
        rigidBody2D.velocity = Vector2.zero;
    }
}
