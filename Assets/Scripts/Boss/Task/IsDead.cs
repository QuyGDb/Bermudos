using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDead : Conditional
{
    private Health health;
    public override void OnAwake()
    {
        health = GetComponent<Health>();
    }

    public override TaskStatus OnUpdate()
    {
        if (health.currentHealth <= 0)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}

