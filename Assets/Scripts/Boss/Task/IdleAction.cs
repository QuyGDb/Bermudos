using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class IdleAction : SkillsAction
{

    public override void OnAwake()
    {
        base.OnAwake();
    }

    public override TaskStatus OnUpdate()
    {
        animator.SetTrigger(bossAnimationStateDic[bossAnimationState]);
        return TaskStatus.Success;
    }

}
