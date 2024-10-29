using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEyeAttackLoop : Action
{
    private RangedSkills rangedSkills;
    [Header("EYE LOOP")]
    public float eyeLoopRate = 0.5f;
    public float eyeLoopDuration = 5f;
    public override void OnAwake()
    {
        rangedSkills = GetComponent<RangedSkills>();
    }

    public override TaskStatus OnUpdate()
    {
        rangedSkills.StartEyeLoop(eyeLoopDuration, eyeLoopRate);
        return TaskStatus.Success;
    }


}
