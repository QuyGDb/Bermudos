using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Beam : MonoBehaviour
{
    private bool isColliding = false;
    private int damageBeam = 20;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        float targetScaleY = Mathf.Lerp(0, 1, animator.GetCurrentAnimatorStateInfo(0).normalizedTime / 1.5f);
        transform.localScale = new Vector3(transform.localScale.x, targetScaleY * 3.5f, transform.localScale.z);
    }

    public void EyeAttackEnd()
    {

        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding)
            return;
        collision.GetComponent<ReceiveDamage>()?.TakeDamage(damageBeam);
        collision.GetComponent<PlayerEffect>()?.DamagePushEfect(transform.position);
        collision.GetComponent<PlayerEffect>()?.CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponentsInChildren<SpriteRenderer>());
        isColliding = true;
    }
}
