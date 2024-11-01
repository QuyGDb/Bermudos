using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPhase1 : Action
{
    private Animator animator;
    public Material material;
    public float transitionTime = 1.0f;
    private float fadeAmount = 0.0f;
    private float transitionTime2;
    private RangedSkills rangedSkills;
    private MeleeSkills meleeSkills;
    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
        rangedSkills = GetComponent<RangedSkills>();
        meleeSkills = GetComponent<MeleeSkills>();
    }
    public override void OnStart()
    {
        rangedSkills.StopAllCoroutines();
        meleeSkills.StopAllCoroutines();
        transitionTime2 = transitionTime;
        animator.SetTrigger(Settings.EyeLoopDeath);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    public override TaskStatus OnUpdate()
    {
        //if (isFirstFrame)
        //{
        //    isFirstFrame = false;
        //    return TaskStatus.Running;
        //}
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
            return TaskStatus.Running;

        if (transitionTime > 0)
        {
            fadeAmount = fadeAmount + (Time.deltaTime / transitionTime2);
            transitionTime -= Time.deltaTime;
            material.SetFloat("_FadeAmount", fadeAmount);
            return TaskStatus.Running;
        }
        return TaskStatus.Success;
    }

}
