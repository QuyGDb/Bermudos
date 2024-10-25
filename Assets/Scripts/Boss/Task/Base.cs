
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class Base : Action
{
    public Animator animator;

    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {

        return TaskStatus.Success;
    }
}
