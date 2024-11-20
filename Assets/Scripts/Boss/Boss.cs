using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public DestroyedEvent destroyedEvent;
    [HideInInspector] public PoiseEvent poiseEvent;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BossEffect bossEffect;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool isPhaseTwo;
    private void Awake()
    {
        // Load Components
        healthEvent = GetComponent<HealthEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        poiseEvent = GetComponent<PoiseEvent>();
        bossEffect = GetComponent<BossEffect>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        GameResources.Instance.boss.SetFloat("_FadeAmount", 0f);
    }


    public IEnumerator BossAttack()
    {

        animator.SetTrigger(Settings.Hop);
        yield return null;
        animator.SetTrigger(Settings.EyeLoopDeath);
    }
    private void HealthEvent_OnHealthLost(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        if (healthEventArgs.healthAmount <= 0)
        {
            EnemyDestroyed();
            healthEvent.OnHealthChanged -= HealthEvent_OnHealthLost;
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


