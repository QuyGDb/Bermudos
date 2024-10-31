using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHealthLow : Conditional
{
    public float healthThreshold = 0.5f;
    private Health health;
    private bool isFirstEnd = true;
    public override void OnAwake()
    {
        health = GetComponent<Health>();
    }

    public override TaskStatus OnUpdate()
    {
        if (health.currentHealth <= health.startingHealth * healthThreshold)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }

    public override void OnEnd()
    {
        Debug.Log(healthThreshold);
        if (!isFirstEnd)
            healthThreshold = 0f;
        isFirstEnd = false;
    }
}
