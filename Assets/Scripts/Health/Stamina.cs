using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private int maxStamina;
    [HideInInspector] public float currentStamina;
    private Player player;
    private float staminaRecoveryTimer;
    private float staminaRecoveryRate = 1f;
    private float staminaRegenAmount = 5f;
    private void Awake()
    {
        player = GetComponent<Player>();

    }

    private void Start()
    {
        currentStamina = maxStamina;
        player.staminaEvent.CallStaminaChangedEvent(currentStamina / maxStamina, currentStamina, 0);
    }
    private void Update()
    {

        RegenerateStamina();
        staminaRecoveryTimer = Time.time + staminaRecoveryRate;

    }

    public void UseStamina(int staminaAmount)
    {
        if (currentStamina > 0)
        {
            currentStamina -= staminaAmount;
            player.staminaEvent.CallStaminaChangedEvent(currentStamina / maxStamina, currentStamina, staminaAmount);
        }
    }

    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenAmount * Time.deltaTime;
            player.staminaEvent.CallStaminaChangedEvent(currentStamina / maxStamina, currentStamina, 0);
        }
    }

}
