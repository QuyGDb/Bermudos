using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPhase2 : Action
{
    private Animator animator;
    private DealContactDamage dealContactDamage;
    public Material material;
    public float fadeInTime = 1f;
    private float fadeInTime2;
    private float fadeInAmount = 1.0f;
    private Rigidbody2D rb;
    private RangedSkills rangedSkills;
    private BossEffect bossEffect;
    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
        dealContactDamage = GetComponent<DealContactDamage>();
        rb = GetComponent<Rigidbody2D>();
        rangedSkills = GetComponent<RangedSkills>();
        bossEffect = GetComponent<BossEffect>();
    }
    public override void OnStart()
    {
        rangedSkills.ammo = rangedSkills.ammoPhase2;
        animator.SetTrigger(Settings.Idle2);
        fadeInTime2 = fadeInTime;
        dealContactDamage.isRemoved = true;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        bossEffect.isPhase2 = true;
    }
    public override TaskStatus OnUpdate()
    {
        if (fadeInTime > 0)
        {
            fadeInAmount = fadeInAmount - (Time.deltaTime / fadeInTime2);

            material.SetFloat("_FadeAmount", fadeInAmount);
            fadeInTime -= Time.deltaTime;
            return TaskStatus.Running;
        }
        gameObject.layer = LayerMask.NameToLayer("Boss");
        return TaskStatus.Success;
    }



}
