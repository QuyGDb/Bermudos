using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEyeAttackLoop : Action
{
    public RangedSkills rangedSkills;

    public override void OnAwake()
    {
        rangedSkills = GetComponent<RangedSkills>();
    }

    public override TaskStatus OnUpdate()
    {
        rangedSkills.StartEyeLoop();
        return TaskStatus.Success;
    }


}
