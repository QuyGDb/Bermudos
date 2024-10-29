using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Settings
{
    #region ANIMATOR PARAMETERS
    // Animator parameters - Player

    public static int isIdle = Animator.StringToHash("isIdle");
    public static int isMoving = Animator.StringToHash("isMove");
    public static int isAttack = Animator.StringToHash("isAttack");
    public static int isFaint = Animator.StringToHash("isFaint");


    // Animator parameters - Aim
    public static int aimUp = Animator.StringToHash("aimUp");
    public static int aimUp1 = Animator.StringToHash("aimUp1");
    public static int aimDown = Animator.StringToHash("aimDown");
    public static int aimUpRight = Animator.StringToHash("aimUpRight");
    public static int aimUpLeft = Animator.StringToHash("aimUpLeft");
    public static int aimRight = Animator.StringToHash("aimRight");
    public static int aimLeft = Animator.StringToHash("aimLeft");
    public static int aimDownRight = Animator.StringToHash("aimDownRight");
    public static int aimDownLeft = Animator.StringToHash("aimDownLeft");

    // Animator parameters - Boss
    public static int isPhase2 = Animator.StringToHash("isPhase2");
    public static int Idle = Animator.StringToHash("Idle");
    public static int ouch = Animator.StringToHash("ouch");
    public static int OpenMouth = Animator.StringToHash("OpenMouth");
    public static int eyeAttack = Animator.StringToHash("eyeAttack");
    public static int eyeLoop = Animator.StringToHash("eyeLoop");
    public static int Hop = Animator.StringToHash("Hop");
    public static int EyeLoopDeath = Animator.StringToHash("EyeLoopDeath");
    public static int Base = Animator.StringToHash("Base");
    public static int Walk = Animator.StringToHash("Walk");
    public static int swipe = Animator.StringToHash("swipe");
    public static int mouthOpenLoop = Animator.StringToHash("mouthOpenLoop");
    public static int spearAtk = Animator.StringToHash("spearAtk");
    public static int spinny = Animator.StringToHash("spinny");
    #endregion

    #region Epsilon
    public const float epsilon = 0.01f;
    #endregion

    #region CONTACT DAMAGE PARAMETERS
    public const float contactDamageCollisionResetDelay = 0.5f;
    #endregion

    public const float dashCooldown = 1f;

    public const float bashCooldown = 1f;
}
