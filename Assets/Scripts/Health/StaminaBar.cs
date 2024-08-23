using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Slider easeStaminaBar;
    float lerpSpeed = 0.05f;

    private void OnEnable()
    {
        GameManager.Instance.player.staminaEvent.OnStaminaChanged += StaminaEvent_OnStaminaChanged;
    }
    private void OnDisable()
    {
        GameManager.Instance.player.staminaEvent.OnStaminaChanged -= StaminaEvent_OnStaminaChanged;
    }

    private void StaminaEvent_OnStaminaChanged(StaminaEvent staminaEvent, StaminaEventArgs staminaEventArgs)
    {
        staminaBar.value = staminaEventArgs.staminaPercent;
    }

    private void Update()
    {
        if (staminaBar.value != easeStaminaBar.value)
            easeStaminaBar.value = Mathf.Lerp(easeStaminaBar.value, staminaBar.value, lerpSpeed);
    }
}
