using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownTimer : MonoBehaviour
{
    [SerializeField] private Image dashCooldownImage;
    [SerializeField] private Image bashCooldownImage;
    [SerializeField] private Image beamCooldownImage;
    [SerializeField] private Image beamIcon;

    public float dashCooldown;
    public float bashCooldown;

    private void OnEnable()
    {
        StaticEventHandler.OnTriggerDash += GetDashCooldown;
        StaticEventHandler.OnTriggerBash += GetBashCooldown;
        RageEvent.OnRageChanged += IncreaseBeamCooldown;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnTriggerDash -= GetDashCooldown;
        StaticEventHandler.OnTriggerBash -= GetBashCooldown;
        RageEvent.OnRageChanged -= IncreaseBeamCooldown;
    }

    public void GetDashCooldown()
    {
        this.dashCooldown = Settings.dashCooldown;
        dashCooldownImage.fillAmount = 1;
    }

    public void GetBashCooldown()
    {
        this.bashCooldown = Settings.bashCooldown;
        bashCooldownImage.fillAmount = 1;
    }
    public void IncreaseBeamCooldown(RageEventArgs rageEventArgs)
    {
        beamCooldownImage.fillAmount = rageEventArgs.ragePercent;
        if (beamCooldownImage.fillAmount == 1)
        {
            beamCooldownImage.gameObject.SetActive(false);
            beamIcon.gameObject.SetActive(true);
        }
        else
        {
            beamCooldownImage.gameObject.SetActive(true);
            beamIcon.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        dashCooldownImage.fillAmount = 0;
        bashCooldownImage.fillAmount = 0;
        beamCooldownImage.fillAmount = 0;
    }

    private void Update()
    {
        if (dashCooldownImage.fillAmount > 0)
        {
            dashCooldownImage.fillAmount -= (1 / dashCooldown) * Time.deltaTime;
        }
        if (bashCooldownImage.fillAmount > 0)
        {
            bashCooldownImage.fillAmount -= (1 / bashCooldown) * Time.deltaTime;
        }
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(dashCooldownImage), this);
        HelperUtilities.ValidateCheckNullValue(this, nameof(bashCooldownImage), this);
        HelperUtilities.ValidateCheckNullValue(this, nameof(beamCooldownImage), this);
        HelperUtilities.ValidateCheckNullValue(this, nameof(beamIcon), this);
    }
#endif
    #endregion
}
