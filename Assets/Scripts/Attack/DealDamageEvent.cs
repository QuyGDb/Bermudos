using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DealDamageEvent : MonoBehaviour
{
    public event Action<DealDamageEvent, DealDamageEventAgrs> OnDealDamage;

    public void CallTakeDamageEvent(int damage)
    {

        OnDealDamage?.Invoke(this, new DealDamageEventAgrs { damage = damage });
    }
}

public class DealDamageEventAgrs : EventArgs
{
    public int damage;
}

