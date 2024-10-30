using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPhase2 : Action
{
    private Animator animator;
    private DealContactDamage dealContactDamage;
    private bool isFirstFrame = true;
    public float fadeInTime = 6f;
    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
        dealContactDamage = GetComponent<DealContactDamage>();
    }
    public override void OnStart()
    {
        isFirstFrame = true;
        dealContactDamage.isRemoved = true;
        animator.SetTrigger(Settings.isPhase2);
        animator.SetTrigger(Settings.Idle);
    }
    public override TaskStatus OnUpdate()
    {

        return TaskStatus.Success;
    }
    public override void OnEnd()
    {
        isFirstFrame = true;
    }

    public IEnumerator Phase2()
    {
        yield return new WaitForSeconds(fadeInTime);
    }
}
