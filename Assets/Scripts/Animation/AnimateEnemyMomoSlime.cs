using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEnemyMomoSlime : MonoBehaviour
{
    private Enemy enemy;
    private AimDirection aimDirection;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        enemy.animateEvent.OnAnimate += Animate;
    }

    private void OnDisable()
    {
        enemy.animateEvent.OnAnimate -= Animate;
    }

    private void Animate(AnimateEvent animateEvent, AnimateEventArgs animateEventArgs)
    {
        InitializeAnimationParameters();
        PlayAnimation(animateEventArgs.aimDirection);
    }

    private void InitializeAnimationParameters()
    {
        enemy.animator.SetBool(Settings.aimDown, false);
        enemy.animator.SetBool(Settings.aimUp, false);
        enemy.animator.SetBool(Settings.aimRight, false);
        enemy.animator.SetBool(Settings.aimLeft, false);
    }

    private void PlayAnimation(AimDirection aimDirection)
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
