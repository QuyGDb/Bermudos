using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOpenMouthLoop : Action
{
    private RangedSkills rangedSkills;

    public override void OnAwake()
    {
        rangedSkills = GetComponent<RangedSkills>();
    }

    public override TaskStatus OnUpdate()
    {
        rangedSkills.StartMouthOpenLoopMouth();
        return TaskStatus.Success;
    }
}
