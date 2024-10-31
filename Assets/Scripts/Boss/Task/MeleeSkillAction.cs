using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MeleeSkillAction : SkillsAction
{
    private MeleeSkills meleeSkills;
    public override void OnAwake()
    {
        base.OnAwake();
        meleeSkills = GetComponent<MeleeSkills>();
    }

    public override void OnStart()
    {
        isFirstUpdate = true;
        meleeSkills.PlayActiveSkill(bossAnimationStateDic[bossAnimationState]);
    }
    public override TaskStatus OnUpdate()
    {
        if (!meleeSkills.isAttacking)
            return TaskStatus.Running;
        return HandleTimeOfHits(normalizedTime);

    }
    public override void OnEnd()
    {
        base.OnEnd();
        meleeSkills.isAttacking = false;
    }
}
