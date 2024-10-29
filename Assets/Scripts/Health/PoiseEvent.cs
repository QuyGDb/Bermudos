using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseEvent : MonoBehaviour
{
    public event Action<PoiseEvent, PoiseEventArgs> OnPoise;

    public void CallPoiseEvent(int poiseAmount, int currentPoise, int maxPoise, int stunTime)
    {
        OnPoise?.Invoke(this, new PoiseEventArgs() { poiseAmount = poiseAmount, currentPoise = currentPoise, maxPoise = maxPoise, stunTime = stunTime });
    }
}
public class PoiseEventArgs
{
    public int poiseAmount;
    public int currentPoise;
    public int maxPoise;
    public int stunTime;
}
