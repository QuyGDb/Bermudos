using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashEvent : MonoBehaviour
{
    public event Action<BashEvent, BashEventArgs> OnBash;

    public void CallOnBashEvent(BashState bashState)
    {
        OnBash?.Invoke(this, new BashEventArgs() { bashState = bashState });
    }
}
public class BashEventArgs : EventArgs
{
    public BashState bashState;
}
