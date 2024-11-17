using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthEvent))]
[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    public int currentHealth;
    public int startingHealth;
    private HealthEvent healthEvent;
    private DieEvent dieEvent;
    private void Awake()
    {
        healthEvent = GetComponent<HealthEvent>();
        dieEvent = GetComponent<DieEvent>();
    }
    private void OnEnable()
    {
        StaticEventHandler.OnRestInBonfire += StaticEventHandler_OnRestInBonfire;
        if (dieEvent != null)
            dieEvent.OnDie += DieEvent_OnDie;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnRestInBonfire -= StaticEventHandler_OnRestInBonfire;
        if (dieEvent != null)
            dieEvent.OnDie -= DieEvent_OnDie;
    }
    private void StaticEventHandler_OnRestInBonfire()
    {
        currentHealth = startingHealth;
    }
    private void DieEvent_OnDie(DieEvent dieEvent)
    {
        currentHealth = 0;
        IncreaseHealth(startingHealth);
    }
    private void Start()
    {
        currentHealth = startingHealth;
        healthEvent.CallHealthChangedEvent(((float)currentHealth / (float)startingHealth), currentHealth, 0);

    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthEvent.CallHealthChangedEvent(((float)currentHealth / (float)startingHealth), currentHealth, damageAmount);
    }
    public void IncreaseHealth(int healthAmount)
    {
        if (currentHealth + healthAmount > startingHealth)
            currentHealth = startingHealth;
        else
            currentHealth += healthAmount;

        healthEvent.CallHealthChangedEvent(((float)currentHealth / (float)startingHealth), currentHealth, 0);
    }
}
