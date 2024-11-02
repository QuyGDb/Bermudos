using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamageAction : Action
{
    private Animator animator;
    private RangedSkills rangedSkills;
    private MeleeSkills meleeSkills;
    private bool isFirstUpdate;
    public SharedFloat stunTime;
    private Poise poise;

    public override void OnAwake()
    {
        poise = GetComponent<Poise>();
        animator = GetComponent<Animator>();
        rangedSkills = GetComponent<RangedSkills>();
        meleeSkills = GetComponent<MeleeSkills>();
    }
    public override void OnStart()
    {
        rangedSkills.StopAllCoroutines();
        meleeSkills.StopAllCoroutines();
        isFirstUpdate = true;
        stunTime.Value = poise.stunTime;
    }
    public override TaskStatus OnUpdate()
    {
        if (poise.currentPoise <= 0 && stunTime.Value > 0)
            stunTime.Value -= Time.deltaTime;
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            return TaskStatus.Running;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            return TaskStatus.Running;
        return TaskStatus.Success;
    }

}
