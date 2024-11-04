using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamByPlayer : MonoBehaviour
{
    public GameObject beamPrefab;
    public Material beamMaterial;
    private Rage rage;
    private AnimateEvent animateEvent;
    Vector3 direction;
    private float ragePercent;
    private void Awake()
    {
        rage = GetComponent<Rage>();
        animateEvent = GetComponent<AnimateEvent>();
    }
    private void OnEnable()
    {
        RageEvent.OnRageChanged += RageEvent_OnRageChanged;
        animateEvent.OnAnimate += AnimateEvent_OnAnimate;
    }
    private void OnDisable()
    {
        RageEvent.OnRageChanged -= RageEvent_OnRageChanged;
        animateEvent.OnAnimate -= AnimateEvent_OnAnimate;
    }

    private void AnimateEvent_OnAnimate(AnimateEvent animateEvent, AnimateEventArgs animateEventArgs)
    {
        switch (animateEventArgs.aimDirection)
        {
            case AimDirection.Up:
                direction = Vector3.up;
                break;
            case AimDirection.Down:
                direction = Vector3.down;
                break;
            case AimDirection.Left:
                direction = Vector3.left;
                break;
            case AimDirection.Right:
                direction = Vector3.right;
                break;
            case AimDirection.UpLeft:
                direction = Vector3.up + Vector3.left;
                break;
            case AimDirection.UpRight:
                direction = Vector3.up + Vector3.right;
                break;
            case AimDirection.DownLeft:
                direction = Vector3.down + Vector3.left;
                break;
            case AimDirection.DownRight:
                direction = Vector3.down + Vector3.right;
                break;
        }
    }
    private void RageEvent_OnRageChanged(RageEventArgs rageEventArgs)
    {
        ragePercent = rageEventArgs.ragePercent;
    }
    public void Beam()
    {
        if (ragePercent < 1) return;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle + Settings.beamRotationOffset);
        GameObject beam = Instantiate(beamPrefab, transform.position, rotation);
        beam.layer = LayerMask.NameToLayer("BashAmmo");
        beam.GetComponent<Beam>().amountScaleY = 10f;
        beam.GetComponent<SpriteRenderer>().material = beamMaterial;
        rage.currentRage = 0;
        RageEvent.CallRageChangedEvent(0, 0, rage.maxRage);
    }
}
