using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SKillCooldownTimer : MonoBehaviour
{
    [SerializeField] private Image dashCooldownImage;
    [SerializeField] private Image bashCooldownImage;
    private float dashCooldown;
    private float bashCooldown;
    public void GetDashCooldown(float dashCooldown)
    {
        this.dashCooldown = dashCooldown;
        dashCooldownImage.fillAmount = 1;
    }

    public void GetBashCooldown(float bashCooldown)
    {
        this.bashCooldown = bashCooldown;
        bashCooldownImage.fillAmount = 1;
    }

    private void Start()
    {
        dashCooldownImage.fillAmount = 0;
        bashCooldownImage.fillAmount = 0;
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
}
