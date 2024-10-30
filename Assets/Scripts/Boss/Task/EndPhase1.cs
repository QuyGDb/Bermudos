using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPhase1 : Action
{
    private Animator animator;
    public Material material;
    private bool isFirstFrame = true;
    private float transitionTime = 6.0f;
    private float fadeAmount = 0.0f;
    private float transitionTime2;
    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
    }
    public override void OnStart()
    {
        Debug.Log("First Frame" + Time.frameCount);
        isFirstFrame = true;
        transitionTime2 = transitionTime;
        animator.SetTrigger(Settings.EyeLoopDeath);
    }
    public override TaskStatus OnUpdate()
    {
        Debug.Log("First Frame" + Time.frameCount);

        if (isFirstFrame)
        {
            isFirstFrame = false;
            return TaskStatus.Running;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
            return TaskStatus.Running;
        fadeAmount = fadeAmount + (Time.deltaTime / transitionTime2);
        if (transitionTime > 0)
        {
            transitionTime -= Time.deltaTime;
            Debug.Log(transitionTime + " t");
            Debug.Log(fadeAmount);
            material.SetFloat("_FadeAmount", fadeAmount);
            return TaskStatus.Running;
        }
        material.SetFloat("_FadeAmount", 0);
        return TaskStatus.Success;
    }

}
