using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimateEvent))]
[RequireComponent(typeof(HealthEvent))]
[RequireComponent(typeof(EnemyStateEvent))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    [HideInInspector] public AnimateEvent animateEvent;
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public EnemyStateEvent enemyStateEvent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Health health;
    [HideInInspector] public DamageEfect damageEfect;
    [HideInInspector] SpriteRenderer spriteRenderer;
    private void Awake()
    {
        // Load Components
        animateEvent = GetComponent<AnimateEvent>();
        healthEvent = GetComponent<HealthEvent>();
        enemyStateEvent = GetComponent<EnemyStateEvent>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        damageEfect = GetComponent<DamageEfect>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
