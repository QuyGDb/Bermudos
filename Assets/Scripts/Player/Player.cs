using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(AnimateEvent))]
[RequireComponent(typeof(AttackEvent))]
[RequireComponent(typeof(DealDamageEvent))]
[RequireComponent(typeof(HealthEvent))]
[RequireComponent(typeof(DestroyedEvent))]
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public AnimateEvent animateEvent;
    [HideInInspector] public AttackEvent attackEvent;
    [HideInInspector] public DealDamageEvent dealDamageEvent;
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public DestroyedEvent destroyedEvent;
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        // Load components
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        idleEvent = GetComponent<IdleEvent>();
        animateEvent = GetComponent<AnimateEvent>();
        attackEvent = GetComponent<AttackEvent>();
        dealDamageEvent = GetComponent<DealDamageEvent>();
        healthEvent = GetComponent<HealthEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        // Subscribe to player health event
        healthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe from player health event
        healthEvent.OnHealthChanged -= HealthEvent_OnHealthChanged;
    }


    private void Start()
    {
        StaticEventHandler.CallPlayerChangedEvent(this);
    }

    public Vector2 GetPlayerPosition()
    {
        return transform.position;
    }
    /// <summary>
    /// Handle health changed event
    /// </summary>
    private void HealthEvent_OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        // If player has died
        if (healthEventArgs.healthAmount <= 0f)
        {
            destroyedEvent.CallDestroyedEvent(true, 0);
        }

    }
}