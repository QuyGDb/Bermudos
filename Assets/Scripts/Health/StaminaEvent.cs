using System;
using UnityEngine;

[DisallowMultipleComponent]
public class StaminaEvent : MonoBehaviour
{
    public event Action<StaminaEvent, StaminaEventArgs> OnStaminaChanged;

    public void CallStaminaChangedEvent(float staminaPercent, float currentStamina, float staminaAmount)
    {
        OnStaminaChanged?.Invoke(this, new StaminaEventArgs() { staminaPercent = staminaPercent, currentStamina = currentStamina, staminaAmount = staminaAmount });

    }
}

public class StaminaEventArgs : EventArgs
{
    public float staminaPercent;
    public float currentStamina;
    public float staminaAmount;
}