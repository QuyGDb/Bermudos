using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinny : Action
{
    private Animator animator;
    Vector2 direction;
    Weapon weapon;
    DealContactDamage dealContactDamage;
    Rigidbody2D rb;
    public float speed = 5f;
    public int damage = 10;
    private bool isColliding = false;
    MeleeSkills meleeSkills;
    BoxCollider2D boxCollider2D;
    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        meleeSkills = GetComponent<MeleeSkills>();
        weapon = GetComponent<Weapon>();
        dealContactDamage = GetComponent<DealContactDamage>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public override void OnStart()
    {
        rb.drag = 0;
        Physics2D.IgnoreCollision(boxCollider2D, GameManager.Instance.player.GetComponent<BoxCollider2D>());
        gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        dealContactDamage.isRemoved = false;
        isColliding = false;
        animator.SetTrigger(Settings.spinny2);
        direction = (GameManager.Instance.player.transform.position - transform.position).normalized;
        dealContactDamage.contactDamageAmount = damage;
    }
    public override void OnFixedUpdate()
    {
        rb.velocity = direction * speed;
    }
    public override TaskStatus OnUpdate()
    {
        if (isColliding)
            return TaskStatus.Success;
        return TaskStatus.Running;

    }
    public override void OnLateUpdate()
    {
        weapon.GetWeaponCollider();
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            return;
        isColliding = true;
    }

    public override void OnEnd()
    {
        rb.drag = 100000;
        Physics2D.IgnoreCollision(boxCollider2D, GameManager.Instance.player.GetComponent<BoxCollider2D>(), false);
        rb.velocity = Vector2.zero;
        dealContactDamage.isRemoved = true;
        gameObject.transform.localScale = new Vector3(3f, 3f, 3f);
    }
}
