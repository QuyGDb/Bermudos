using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DealDamageEvent : MonoBehaviour
{
    public event Action<DealDamageEvent, DealDamageEventArgs> OnDealDamage;

    public void CallTakeDamageEvent(bool isDealDamage)
    {
        OnDealDamage?.Invoke(this, new DealDamageEventArgs { isDealDamage = isDealDamage });
    }

}
public class DealDamageEventArgs : EventArgs
{
    public bool isDealDamage;
}
