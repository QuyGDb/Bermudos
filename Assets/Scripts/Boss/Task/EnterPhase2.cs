using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPhase2 : Action
{
    private Animator animator;
    public Material material;
    private bool isFirstFrame = true;
    private float transitionTime = 3.0f;
    private float fadeAmount = 0.0f;
    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
    }
    public override void OnStart()
    {
        isFirstFrame = true;
        animator.SetTrigger(Settings.EyeLoopDeath);
    }
    public override TaskStatus OnUpdate()
    {
        if (isFirstFrame)
        {
            isFirstFrame = false;
            return TaskStatus.Running;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
            return TaskStatus.Running;
        fadeAmount = Mathf.Lerp(fadeAmount, 3, Time.deltaTime) / transitionTime;
        if (transitionTime > 0)
        {
            transitionTime -= Time.deltaTime;
            material.SetFloat("_FadeAmount", fadeAmount);
            return TaskStatus.Running;
        }
        animator.SetBool(Settings.isPhase2, true);
        return TaskStatus.Success;
    }

}
