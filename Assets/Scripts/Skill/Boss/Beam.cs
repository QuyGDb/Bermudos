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
    private LayerMask enemyLayerMask;
    private LayerMask bossLayerMask;
    private LayerMask playerLayerMask;
    [HideInInspector] public float amountScaleY;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyLayerMask = LayerMask.GetMask("Enemy");
        bossLayerMask = LayerMask.GetMask("Boss");
        playerLayerMask = LayerMask.GetMask("Player");
    }
    private void LateUpdate()
    {
        float targetScaleY = Mathf.Lerp(0, 1, animator.GetCurrentAnimatorStateInfo(0).normalizedTime / 1.5f);
        transform.localScale = new Vector3(transform.localScale.x, targetScaleY * amountScaleY, transform.localScale.z);
    }

    public void EyeAttackEnd()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding)
            return;
        if ((playerLayerMask.value & 1 << collision.gameObject.layer) > 0)
        {
            collision.GetComponent<PlayerEffect>().DamagePushEfect(transform.position);
            collision.GetComponent<PlayerEffect>().CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponentsInChildren<SpriteRenderer>());
        }
        collision.GetComponent<ReceiveDamage>()?.TakeDamage(damageBeam);
        if ((enemyLayerMask.value & 1 << collision.gameObject.layer) > 0)
        {
            collision.GetComponent<EnemyEffect>()?.DamagePushEfect(transform.position);
            collision.GetComponent<EnemyEffect>()?.CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponentsInChildren<SpriteRenderer>());
        }
        if ((bossLayerMask.value & 1 << collision.gameObject.layer) > 0)
        {
            collision.GetComponent<BossEffect>()?.PushBossByWeapon(transform.position);
            collision.GetComponent<BossEffect>()?.OuchEffect();
        }
        isColliding = true;
    }
}
