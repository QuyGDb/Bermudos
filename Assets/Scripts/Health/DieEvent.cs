using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEvent : MonoBehaviour
{
    public event Action<DieEvent> OnDie;

    public void CallDieEvent()
    {
        OnDie?.Invoke(this);
    }
}
