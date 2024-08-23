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
    private Rigidbody2D rb;
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
        rb = GetComponent<Rigidbody2D>();
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
    private void Update()
    {
        // rb.velocity = Vector2.zero;
    }
    /// <summary>
    /// Handle health lost event
    /// </summary>
    private void HealthEvent_OnHealthLost(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {

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
