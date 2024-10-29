using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamageAction : Action
{
    private Animator animator;
    private RangedSkills rangedSkills;
    private bool isFirstUpdate;
    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
        rangedSkills = GetComponent<RangedSkills>();
    }
    public override void OnStart()
    {
        rangedSkills.StopAllCoroutines();
        isFirstUpdate = true;
    }
    public override TaskStatus OnUpdate()
    {

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
