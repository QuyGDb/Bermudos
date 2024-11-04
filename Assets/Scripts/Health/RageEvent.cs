using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RageEvent
{
    public static event Action<RageEventArgs> OnRageChanged;

    public static void CallRageChangedEvent(float ragePercent, float currentRage, float rageAmount)
    {
        OnRageChanged?.Invoke(new RageEventArgs() { ragePercent = ragePercent, currentRage = rageAmount, maxRage = rageAmount });

    }
}

public class RageEventArgs
{
    public float ragePercent;
    public float currentRage;
    public float maxRage;
}
