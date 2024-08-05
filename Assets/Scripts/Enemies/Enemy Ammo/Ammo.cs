using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IFireable
{
    private Vector3 shootDirection;
    private DamageEfect damageEfects;
    private DestroyedEvent destroyedEvent;
    private float speed;
    private int damage;

    private void Awake()
    {
        damageEfects = GetComponent<DamageEfect>();
        destroyedEvent = GetComponent<DestroyedEvent>();
    }
    private void Update()
    {
        transform.position += shootDirection * speed * Time.deltaTime;
    }

    public void InitialiseAmmo(AmmoDetailsSO ammoDetailsSO, Vector3 shoorDirection)
    {
        this.speed = ammoDetailsSO.speed;
        this.damage = ammoDetailsSO.damage;
        SetShootDirection(shoorDirection);

        gameObject.SetActive(true);
    }

    public void SetShootDirection(Vector3 shootDirection)
    {

        this.shootDirection = shootDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            StaticEventHandler.CallAmmoChangedEvent(this);
            collision.GetComponent<DamageEfect>().DamagePushEfect(true);
            collision.GetComponent<DamageEfect>().CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponentsInChildren<SpriteRenderer>());
        }

        AmmoHitEffect();
        DisableAmmo();
    }

    private void AmmoHitEffect()
    {
        AmmoHitEffect ammoHitEffect = PoolManager.Instance.ReuseComponent(GameResources.Instance.ammoHitEffectPrefab, transform.position, Quaternion.identity) as AmmoHitEffect;
        ammoHitEffect.InitialiseAmmoHitEffect();
        ammoHitEffect.gameObject.SetActive(true);
    }

    private void DisableAmmo()
    {
        gameObject.SetActive(false);
    }
}

