using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AnimateEvent : MonoBehaviour
{
    public event Action<AnimateEvent, AnimateEventArgs> OnAnimate;

    public void CallAnimateEvent(AimDirection aimDirection)
    {
        OnAnimate?.Invoke(this, new AnimateEventArgs() { aimDirection = aimDirection });
    }
}
public class AnimateEventArgs : EventArgs
{
    public AimDirection aimDirection;
}
