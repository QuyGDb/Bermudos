using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsReceiveDamage : Conditional
{
    private HealthEvent healthEvent;
    private PoiseEvent poiseEvent;
    private bool isReceiveDamage = false;
    public SharedFloat stunTime;
    public override void OnAwake()
    {
        healthEvent = GetComponent<HealthEvent>();
        poiseEvent = GetComponent<PoiseEvent>();
        healthEvent.OnHealthChanged += OnHealthChanged;
        poiseEvent.OnPoise += OnPoiseChanged;
    }

    private void OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        isReceiveDamage = true;
    }
    private void OnPoiseChanged(PoiseEvent poiseEvent, PoiseEventArgs poiseEventArgs)
    {
        stunTime.Value = poiseEventArgs.stunTime;
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

