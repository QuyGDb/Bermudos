using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private Player player;
    private bool isDealDamage = false;
    private bool hasAttack = false;

    private void OnEnable()
    {
        player.dealDamageEvent.OnDealDamage += DealDamageEvent_OnDealDamage;
    }
    private void OnDisable()
    {
        player.dealDamageEvent.OnDealDamage -= DealDamageEvent_OnDealDamage;
    }

    private void DealDamageEvent_OnDealDamage(DealDamageEvent dealDamageEvent)
    {
        if (isDealDamage == false)
        {
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
        }
        else if (!isDealDamage && hasAttack)
        {
            hasAttack = false;
        }
    }
}
