using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public DestroyedEvent destroyedEvent;
    [HideInInspector] public PoiseEvent poiseEvent;
    [HideInInspector] public BossEffect bossEffect;
    [HideInInspector] public Animator animator;
    private void Awake()
    {
        // Load Components
        healthEvent = GetComponent<HealthEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        poiseEvent = GetComponent<PoiseEvent>();
        bossEffect = GetComponent<BossEffect>();
        animator = GetComponent<Animator>();
    }
}
