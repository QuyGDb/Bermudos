using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimateEvent))]
[RequireComponent(typeof(HealthEvent))]
[RequireComponent(typeof(EnemyStateEvent))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyEffect))]
[RequireComponent(typeof(PoiseEvent))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(MoveByEnemyAIEvent))]
[RequireComponent(typeof(AttackEvent))]
[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    [HideInInspector] public AnimateEvent animateEvent;
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public EnemyStateEvent enemyStateEvent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Health health;
    [HideInInspector] public EnemyEffect enemyEffect;
    [HideInInspector] public PoiseEvent poiseEvent;
    [HideInInspector] SpriteRenderer spriteRenderer;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MoveByEnemyAIEvent moveByEnemyAIEvent;
    [HideInInspector] public AttackEvent attackEvent;
    [HideInInspector] public EnemyAI enemyAI;
    public AnimationEnemyType animationEnemyType;

    private void Awake()
    {
        // Load Components
        animateEvent = GetComponent<AnimateEvent>();
        healthEvent = GetComponent<HealthEvent>();
        enemyStateEvent = GetComponent<EnemyStateEvent>();
        poiseEvent = GetComponent<PoiseEvent>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        enemyEffect = GetComponent<EnemyEffect>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        idleEvent = GetComponent<IdleEvent>();
        moveByEnemyAIEvent = GetComponent<MoveByEnemyAIEvent>();
        enemyAI = GetComponent<EnemyAI>();
        attackEvent = GetComponent<AttackEvent>();
    }

    private void OnEnable()
    {
        //subscribe to health event
        healthEvent.OnHealthChanged += HealthEvent_OnHealthLost;
    }

    private void OnDisable()
    {
        //subscribe to health event
        healthEvent.OnHealthChanged -= HealthEvent_OnHealthLost;
    }

    /// <summary>
    /// Handle health lost event
    /// </summary>
    private void HealthEvent_OnHealthLost(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        Debug.Log(healthEventArgs.healthAmount);
        if (healthEventArgs.healthAmount <= 0)
        {

            EnemyDestroyed();
        }
    }

    /// <summary>
    /// Enemy destroyed
    /// </summary>
    private void EnemyDestroyed()
    {
        DestroyedEvent destroyedEvent = GetComponent<DestroyedEvent>();
        destroyedEvent.CallDestroyedEvent(new DestroyedEventArgs { playerDied = false });
    }
}
