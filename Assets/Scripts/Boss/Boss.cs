using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public DestroyedEvent destroyedEvent;
    [HideInInspector] public PoiseEvent poiseEvent;
    private Poise poise;
    private float stunTime;
    [HideInInspector] public BossEffect bossEffect;
    [HideInInspector] public Animator animator;
    public Material material;
    private void Awake()
    {
        // Load Components
        healthEvent = GetComponent<HealthEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        poiseEvent = GetComponent<PoiseEvent>();
        bossEffect = GetComponent<BossEffect>();
        animator = GetComponent<Animator>();
        poise = GetComponent<Poise>();
    }
    private void OnEnable()
    {
        // Subscribe to player health event
        healthEvent.OnHealthChanged += HealthEvent_OnHealthLost;
    }

    private void OnDisable()
    {
        // Unsubscribe from player health event
        healthEvent.OnHealthChanged -= HealthEvent_OnHealthLost;
    }
    private void Start()
    {
        StaticEventHandler.CallBossChangedEvent(this);
        stunTime = poise.stunTime;
    }
    private void Update()
    {
        if (poise.currentPoise <= 0 && stunTime > 0)
        {
            animator.SetTrigger(Settings.Hop);
            stunTime -= Time.deltaTime;
        }
        else if (stunTime <= 0)
        {
            poise.currentPoise = poise.maxPoise;
            stunTime = poise.stunTime;
        }
        //material.SetFloat("_FadeAmount", Time.deltaTime);

    }
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
        destroyedEvent.CallDestroyedEvent(new DestroyedEventArgs { bossDied = true });
    }
}


