using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class IsReceiveDamage : Conditional
{
    private HealthEvent healthEvent;
    private bool isReceiveDamage = false;
    public override void OnAwake()
    {
        healthEvent = GetComponent<HealthEvent>();
        healthEvent.OnHealthChanged += OnHealthChanged;

    }

    private void OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        isReceiveDamage = true;
    }

    public override TaskStatus OnUpdate()
    {
        return isReceiveDamage ? TaskStatus.Success : TaskStatus.Failure;
    }
    public override void OnEnd()
    {
        isReceiveDamage = false;
    }
}

