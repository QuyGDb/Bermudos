using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ReceiveDamage : MonoBehaviour
{
    #region Header
    [Header("The damage amount to receive")]
    #endregion
    [SerializeField] private int damageAmount;
    private Health health;
    public bool isNotReceivingDamage = false;

    private void Awake()
    {
        //Load components
        health = GetComponent<Health>();
    }

    public void TakeDamage(int damageAmount = 0)
    {
        if (isNotReceivingDamage) return;
        if (this.damageAmount > 0)
            damageAmount = this.damageAmount;
        health.TakeDamage(damageAmount);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(damageAmount), damageAmount, true);
    }
#endif

    #endregion
}