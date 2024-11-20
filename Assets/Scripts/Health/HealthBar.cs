using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider easeHealthBar;
    float lerpSpeed = 0.05f;

    private void OnEnable()
    {
        GameManager.Instance.player.healthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
    }
    private void OnDisable()
    {
        GameManager.Instance.player.healthEvent.OnHealthChanged -= HealthEvent_OnHealthChanged;
    }

    private void HealthEvent_OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        healthBar.value = healthEventArgs.healthPercent;
    }

    private void Update()
    {
        if (healthBar.value != easeHealthBar.value)
            easeHealthBar.value = Mathf.Lerp(easeHealthBar.value, healthBar.value, lerpSpeed);
    }
}
