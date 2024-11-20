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
    public static int Idle2 = Animator.StringToHash("Idle2");
    public static int ouch2 = Animator.StringToHash("ouch2");
    public static int OpenMouth2 = Animator.StringToHash("OpenMouth2");
    public static int eyeAttack2 = Animator.StringToHash("eyeAttack2");
    public static int eyeLoop2 = Animator.StringToHash("eyeLoop2");
    public static int Hop2 = Animator.StringToHash("Hop2");
    public static int EyeLoopDeath2 = Animator.StringToHash("EyeLoopDeath2");
    public static int Base2 = Animator.StringToHash("Base2");
    public static int Walk2 = Animator.StringToHash("Walk2");
    public static int swipe2 = Animator.StringToHash("swipe2");
    public static int mouthOpenLoop2 = Animator.StringToHash("mouthOpenLoop2");
    public static int spearAtk2 = Animator.StringToHash("spearAtk2");
    public static int spinny2 = Animator.StringToHash("spinny2");
    #endregion

    #region Epsilon
    public const float epsilon = 0.01f;
    #endregion

    #region CONTACT DAMAGE PARAMETERS
    public const float contactDamageCollisionResetDelay = 0.5f;
    #endregion

    public const float dashCooldown = 1f;
    public const float rageAmount = 1f;
    public const float bashCooldown = 0.5f;

    public const float beamRotationOffset = 90f;

    #region INVENTORY
    public const int inventorySlotQuantity = 70;
    public const int hotBarSlotQuantity = 10;
    #endregion

    public const int maxStack = 10;

    #region Camera
    public const float orthoSize = 8.4375f;
    #endregion

    #region AUDIO
    public const float musicFadeOutTime = 0.5f;  // Defualt Music Fade Out Transition
    public const float musicFadeInTime = 0.5f;  // Default Music Fade In Transition
    #endregion

    #region Stamina
    public const int attackCost = 10;
    #endregion
}
