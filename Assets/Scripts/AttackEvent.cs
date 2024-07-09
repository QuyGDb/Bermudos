using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AttackEvent : MonoBehaviour
{
    public event Action<AttackEvent> OnAttack;
    public void CallAttackEvent()
    {
        OnAttack?.Invoke(this);
    }
}

