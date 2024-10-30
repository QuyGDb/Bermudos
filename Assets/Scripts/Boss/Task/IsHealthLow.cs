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
        Debug.Log("Tick 0: " + Time.frameCount);
        if (health.currentHealth <= health.startingHealth * healthThreshold)
        {
            healthThreshold = 0f;
            Debug.Log("Tick 1: " + Time.frameCount);
            return TaskStatus.Success;
        }
        else
        {
            Debug.Log("Tick 2: " + Time.frameCount);
            return TaskStatus.Failure;

        }
    }
}
