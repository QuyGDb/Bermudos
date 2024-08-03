using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IFireable
{
    private float speed;
    private int damage;
    private Vector3 shootDirection;

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
        }
        AmmoHitEffect();
        DisableAmmo();
    }

    private void AmmoHitEffect()
    {

    }
    private void DisableAmmo()
    {
        gameObject.SetActive(false);
    }
}

