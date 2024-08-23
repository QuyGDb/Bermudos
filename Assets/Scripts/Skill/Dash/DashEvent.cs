using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEvent : MonoBehaviour
{
    public event Action<DashEvent, DashEventArgs> OnDashEvent;

    public void CallDashEvent(Vector2 direction)
    {
        OnDashEvent?.Invoke(this, new DashEventArgs() { direction = direction });
    }
}
public class DashEventArgs : EventArgs
{
    public Vector2 direction;
}
