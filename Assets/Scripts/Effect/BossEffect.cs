using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : Effect
{
    public bool isPhase2 = false;
    private Animator animator;
    private PoiseEvent poiseEvent;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        poiseEvent = GetComponent<PoiseEvent>();
    }
    private void OnEnable()
    {
        poiseEvent.OnPoise += PoiseEvent_onPoiseEvent;
    }
    private void OnDisable()
    {
        poiseEvent.OnPoise -= PoiseEvent_onPoiseEvent;
    }
    private void PoiseEvent_onPoiseEvent(PoiseEvent poiseEvent, PoiseEventArgs poiseEventArgs)
    {
        if (poiseEventArgs.currentPoise <= 0)
        {
            StunEffect(poiseEventArgs.stunTime);
        }
    }
    public void PushBossByWeapon(Vector3 playerPosition)
    {
        Vector3 targetPosition = transform.position + (damageForce * (transform.position - playerPosition).normalized);
        StartCoroutine(PushEnemyByWeaponCoroutine(targetPosition));
    }

    private IEnumerator PushEnemyByWeaponCoroutine(Vector3 targetPosition)
    {
        Vector3 startPosition = rb.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Vector3 lerp = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            rb.MovePosition(lerp);

            elapsedTime += Time.fixedDeltaTime;
            yield return waitForFixedUpdate;
        }
        yield return waitForFixedUpdate;
    }

    public void ouchEffect()
    {
        if (isPhase2)
            animator.SetTrigger(Settings.ouch2);
        else
            animator.SetTrigger(Settings.ouch);
    }
}
