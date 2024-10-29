using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class IdleAction : Action
{
    private Animator animator;

    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        animator.SetBool(Settings.Idle, true);
        return TaskStatus.Success;
    }

}
