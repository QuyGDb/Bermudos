using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthEvent))]
[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    [HideInInspector] public int currentHealth;
    public int startingHealth;
    private HealthEvent healthEvent;

    private void Awake()
    {
        healthEvent = GetComponent<HealthEvent>();
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
}
