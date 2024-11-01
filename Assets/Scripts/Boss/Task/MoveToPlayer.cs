using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : Action
{
    public float timeToMove = 3f;
    private Vector2 direction;
    private Rigidbody2D rb;
    public float speed = 10f;
    private Animator animator;
    public override void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public override void OnStart()
    {
        animator.SetTrigger(Settings.Walk2);
    }
    public override TaskStatus OnUpdate()
    {
        if (timeToMove > 0)
        {
            direction = (GameManager.Instance.player.transform.position - transform.position).normalized;
            timeToMove -= Time.deltaTime;
            rb.velocity = direction * speed;
            return TaskStatus.Running;
        }
        return TaskStatus.Success;
    }
}
