using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DealDamageEvent : MonoBehaviour
{
    public event Action<DealDamageEvent> OnDealDamage;

    public void CallTakeDamageEvent()
    {
        OnDealDamage?.Invoke(this);
    }

}

