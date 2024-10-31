using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PoiseEvent : MonoBehaviour
{
    public event Action<PoiseEvent, PoiseEventArgs> OnPoise;

    public void CallPoiseEvent(float poiseAmount, float currentPoise, float maxPoise, float stunTime)
    {
        OnPoise?.Invoke(this, new PoiseEventArgs() { poiseAmount = poiseAmount, currentPoise = currentPoise, maxPoise = maxPoise, stunTime = stunTime });
    }
}
public class PoiseEventArgs
{
    public float poiseAmount;
    public float currentPoise;
    public float maxPoise;
    public float stunTime;
}
