using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOpenMouthLoop : Action
{
    private RangedSkills rangedSkills;
    public float mouthOpenLoopRate = 0.1f;
    public float mouthOpenLoopDuration = 5f;
    public override void OnAwake()
    {
        rangedSkills = GetComponent<RangedSkills>();
    }

    public override TaskStatus OnUpdate()
    {
        rangedSkills.StartMouthOpenLoopMouth(mouthOpenLoopRate, mouthOpenLoopDuration);
        return TaskStatus.Success;
    }
}
