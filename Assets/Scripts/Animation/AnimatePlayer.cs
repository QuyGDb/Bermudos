using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(AnimateEvent))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(AttackEvent))]
[DisallowMultipleComponent]
public class AnimatePlayer : MonoBehaviour
{
    private Player player;
    private Coroutine attackAnimationCoroutine;
    private bool isAttacking;
    private void Awake()
    {
        // Load components
        player = GetComponent<Player>();
        // Initialize aim animation parameters
        player.animator.SetBool(Settings.aimDown, true);
    }

    private void OnEnable()
    {
        // Subscribe to animate event
        player.animateEvent.OnAnimate += AnimatePlayer_OnAnimate;

        player.idleEvent.OnIdle += IdleEvent_OnIdle;

        player.movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;

        player.attackEvent.OnAttack += AttackEvent_OnAttack;
    }
    private void OnDisable()
    {
        // Unsubscribe from animate event
        player.animateEvent.OnAnimate -= AnimatePlayer_OnAnimate;

        player.idleEvent.OnIdle -= IdleEvent_OnIdle;

        player.movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;

        player.attackEvent.OnAttack -= AttackEvent_OnAttack;
    }

    private void AnimatePlayer_OnAnimate(AnimateEvent animateEvent, AnimateEventArgs animateEventArgs)
    {
        InitializeAimAnimationParameters();

        SetAimAnimationParameters(animateEventArgs.aimDirection);
    }

    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        if (attackAnimationCoroutine == null)
        {
            InitializeStateAnimationParameters();
            player.animator.SetBool(Settings.isIdle, true);
        }
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityArgs movementByVelocityArgs)
    {
        if (attackAnimationCoroutine == null)
        {
            InitializeStateAnimationParameters();
            player.animator.SetBool(Settings.isMoving, true);
        }
    }

    private void AttackEvent_OnAttack(AttackEvent attackEvent, AttackEventArgs attackEventArgs)
    {

        InitializeStateAnimationParameters();
        player.animator.SetBool(Settings.isAttack, true);
        PlayAttackAnimation(attackEventArgs.isAttacking);
    }
    private void InitializeAimAnimationParameters()
    {
        player.animator.SetBool(Settings.aimUp, false);
        player.animator.SetBool(Settings.aimUpRight, false);
        player.animator.SetBool(Settings.aimUpLeft, false);
        player.animator.SetBool(Settings.aimRight, false);
        player.animator.SetBool(Settings.aimLeft, false);
        player.animator.SetBool(Settings.aimDown, false);
        player.animator.SetBool(Settings.aimDownRight, false);
        player.animator.SetBool(Settings.aimDownLeft, false);

    }
    private void InitializeStateAnimationParameters()
    {
        player.animator.SetBool(Settings.isIdle, false);
        player.animator.SetBool(Settings.isMoving, false);
        player.animator.SetBool(Settings.isAttack, false);
        player.animator.SetBool(Settings.isFaint, false);
    }
    private void SetAimAnimationParameters(AimDirection aimDirection)
    {
        switch (aimDirection)
        {
            case AimDirection.Up:
                player.animator.SetBool(Settings.aimUp, true);
                break;
            case AimDirection.UpRight:
                player.animator.SetBool(Settings.aimUpRight, true);
                break;
            case AimDirection.UpLeft:
                player.animator.SetBool(Settings.aimUpLeft, true);
                break;
            case AimDirection.Right:
                player.animator.SetBool(Settings.aimRight, true);
                break;
            case AimDirection.Left:
                player.animator.SetBool(Settings.aimLeft, true);
                break;
            case AimDirection.Down:
                player.animator.SetBool(Settings.aimDown, true);
                break;
            case AimDirection.DownRight:
                player.animator.SetBool(Settings.aimDownRight, true);
                break;
            case AimDirection.DownLeft:
                player.animator.SetBool(Settings.aimDownLeft, true);
                break;
        }
    }
    public void PlayAttackAnimation(bool isAttacking)
    {
        if (attackAnimationCoroutine != null)
        {
            StopCoroutine(attackAnimationCoroutine);
        }
        attackAnimationCoroutine = StartCoroutine(PlayAnimationCoroutine());

    }

    private IEnumerator PlayAnimationCoroutine()
    {
        // Wait until the current animation has finished playing
        while (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
        {
            // Call take damage event
            player.dealDamageEvent.CallTakeDamageEvent(true);
            yield return null;
        }
        // Animation has finished
        attackAnimationCoroutine = null;
        // Call take damage event
        player.dealDamageEvent.CallTakeDamageEvent(false);
    }
}
