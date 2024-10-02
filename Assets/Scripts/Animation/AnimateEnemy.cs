using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEnemy : MonoBehaviour
{
    private Enemy enemy;
    private AimDirection aimDirection;
    private Coroutine attackAnimationCoroutine;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        enemy.animateEvent.OnAnimate += AnimateEnemy_OnAnimate;
        enemy.idleEvent.OnIdle += IdleEvent_OnIdle;
        enemy.moveByEnemyAIEvent.OnMoveByEnemyAI += MoveByEnemyAIEvent_OnMoveByEnemyAI;
        enemy.attackEvent.OnAttack += AttackEvent_OnAttack;
    }

    private void OnDisable()
    {
        enemy.animateEvent.OnAnimate -= AnimateEnemy_OnAnimate;
        enemy.idleEvent.OnIdle -= IdleEvent_OnIdle;
        enemy.moveByEnemyAIEvent.OnMoveByEnemyAI -= MoveByEnemyAIEvent_OnMoveByEnemyAI;
        enemy.attackEvent.OnAttack -= AttackEvent_OnAttack;
    }

    private void MoveByEnemyAIEvent_OnMoveByEnemyAI(MoveByEnemyAIEvent moveByEnemyAIEvent)
    {
        if (attackAnimationCoroutine == null)
        {
            Debug.Log("MoveByEnemyAIEvent_OnMoveByEnemyAI");
            if (enemy.animationEnemyType == AnimationEnemyType.IdleAndRun || enemy.animationEnemyType == AnimationEnemyType.IdleRunAndAttack)
            {

                InitializeStateAnimationParameters();
                enemy.animator.SetBool(Settings.isMoving, true);
            }
        }

    }
    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        if (enemy.animationEnemyType == AnimationEnemyType.IdleAndRun || enemy.animationEnemyType == AnimationEnemyType.IdleRunAndAttack)
        {
            Debug.Log("IdleEvent_OnIdle");
            InitializeStateAnimationParameters();
            enemy.animator.SetBool(Settings.isIdle, true);
        }
    }

    private void AttackEvent_OnAttack(AttackEvent attackEvent)
    {
        if (enemy.animationEnemyType == AnimationEnemyType.IdleRunAndAttack)
        {
            PlayAttackAnimation();
            InitializeStateAnimationParameters();
            enemy.animator.SetBool(Settings.isAttack, true);

        }
    }
    private void PlayAttackAnimation()
    {
        if (attackAnimationCoroutine != null)
        {
            StopCoroutine(attackAnimationCoroutine);
        }
        attackAnimationCoroutine = StartCoroutine(PlayAttackAnimationCoroutine());
    }
    private IEnumerator PlayAttackAnimationCoroutine()
    {
        yield return null;
        while (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        attackAnimationCoroutine = null;
    }
    private void InitializeStateAnimationParameters()
    {
        enemy.animator.SetBool(Settings.isIdle, false);
        enemy.animator.SetBool(Settings.isMoving, false);
        enemy.animator.SetBool(Settings.isAttack, false);
    }

    private void AnimateEnemy_OnAnimate(AnimateEvent animateEvent, AnimateEventArgs animateEventArgs)
    {
        InitializeAnimationParameters();
        SetAimAnimationParameters(animateEventArgs.aimDirection);
    }

    private void InitializeAnimationParameters()
    {
        enemy.animator.SetBool(Settings.aimDown, false);
        enemy.animator.SetBool(Settings.aimUp, false);
        enemy.animator.SetBool(Settings.aimRight, false);
        enemy.animator.SetBool(Settings.aimLeft, false);
    }

    private void SetAimAnimationParameters(AimDirection aimDirection)
    {
        switch (aimDirection)
        {
            case AimDirection.Down:
                enemy.animator.SetBool(Settings.aimDown, true);
                break;
            case AimDirection.Up:
                enemy.animator.SetBool(Settings.aimUp, true);
                break;
            case AimDirection.Right:
                enemy.animator.SetBool(Settings.aimRight, true);
                break;
            case AimDirection.Left:
                enemy.animator.SetBool(Settings.aimLeft, true);
                break;
        }
    }
}
