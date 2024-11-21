using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Rage : MonoBehaviour
{
    [HideInInspector] public float maxRage = 10;
    [HideInInspector] public float currentRage = 0;

    public void IncreaseRage(float amount)
    {
        currentRage += amount;
        if (currentRage > maxRage)
            currentRage = maxRage;
        RageEvent.CallRageChangedEvent((currentRage / maxRage), currentRage, maxRage);
    }
}
