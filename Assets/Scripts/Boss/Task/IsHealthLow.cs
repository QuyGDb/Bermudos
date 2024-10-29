using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHealthLow : Conditional
{
    public float healthThreshold = 0.5f;
    private Health health;
    public override void OnAwake()
    {
        health = GetComponent<Health>();
    }
    public override TaskStatus OnUpdate()
    {
        return health.currentHealth <= health.startingHealth * healthThreshold ? TaskStatus.Success : TaskStatus.Failure;
    }
}
