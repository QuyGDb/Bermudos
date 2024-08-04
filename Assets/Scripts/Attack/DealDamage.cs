using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private DamagePushEfectEvent damagePushEfectEvent;
    private bool isDealDamage = false;
    private bool hasAttack = false;
    private int damage;

    private void OnEnable()
    {
        player.dealDamageEvent.OnDealDamage += DealDamageEvent_OnDealDamage;
    }
    private void OnDisable()
    {
        player.dealDamageEvent.OnDealDamage -= DealDamageEvent_OnDealDamage;
    }

    private void DealDamageEvent_OnDealDamage(DealDamageEvent dealDamageEvent, DealDamageEventAgrs dealDamageEventAgrs)
    {
        if (isDealDamage == false)
        {
            damage = dealDamageEventAgrs.damage;
            isDealDamage = true;
        }
        else
        {
            isDealDamage = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDealDamage && !hasAttack)
        {
            hasAttack = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {

                // collision.GetComponent<Enemy>().damageEfect.DamagePushEfect();
                collision.GetComponent<Enemy>().health.TakeDamage(damage);
                collision.GetComponent<Enemy>().damageEfect.CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponent<SpriteRenderer>());
                damagePushEfectEvent.CallDamagePushEfectEvent();
            }
        }
        else if (!isDealDamage && hasAttack)
        {
            hasAttack = false;
        }
    }
}
