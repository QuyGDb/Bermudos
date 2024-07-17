using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private Player player;
    private bool isDealDamage;
    private void OnEnable()
    {
        player.dealDamageEvent.OnDealDamage += DealDamageEvent_OnDealDamage;
    }
    private void OnDisable()
    {
        player.dealDamageEvent.OnDealDamage -= DealDamageEvent_OnDealDamage;
    }

    private void DealDamageEvent_OnDealDamage(DealDamageEvent dealDamageEvent, DealDamageEventArgs dealDamageEventArgs)
    {
        isDealDamage = dealDamageEventArgs.isDealDamage;
        Debug.Log(isDealDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDealDamage)
            Debug.Log("Deal Damage");
    }

}
