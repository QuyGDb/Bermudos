

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider easeHealthBar;
    float lerpSpeed = 0.05f;
    private HealthEvent healthEvent;
    private void Start()
    {
        StaticEventHandler.OnBossChanged += StaticEventHandler_OnBossChanged;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnBossChanged -= StaticEventHandler_OnBossChanged;
        if (healthEvent != null)
            healthEvent.OnHealthChanged -= HealthEvent_OnHealthChanged;
    }
    private void StaticEventHandler_OnBossChanged(OnBossChangedEventArgs onBossChangedEventArgs)
    {
        healthEvent = onBossChangedEventArgs.boss.healthEvent;
        healthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
        Debug.Log("BossHealthBar: " + healthEvent);
    }
    private void HealthEvent_OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        Debug.Log("BossHealthBar: " + healthEventArgs.healthPercent);
        healthBar.value = healthEventArgs.healthPercent;
    }

    private void Update()
    {
        if (healthBar.value != easeHealthBar.value)
            easeHealthBar.value = Mathf.Lerp(easeHealthBar.value, healthBar.value, lerpSpeed);
    }
}
