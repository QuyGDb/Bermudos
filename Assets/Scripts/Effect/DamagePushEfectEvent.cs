using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePushEfectEvent : MonoBehaviour
{

    public event Action<DamagePushEfectEvent> OnDamagePushEfect;

    public void CallDamagePushEfectEvent()
    {
        OnDamagePushEfect?.Invoke(this);
    }

}

