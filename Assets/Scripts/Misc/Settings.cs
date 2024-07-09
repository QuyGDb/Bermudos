using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    #region ANIMATOR PARAMETERS
    // Animator parameters - Player
    public static int aimUp = Animator.StringToHash("aimUp");
    public static int aimDown = Animator.StringToHash("aimDown");
    public static int aimUpRight = Animator.StringToHash("aimUpRight");
    public static int aimUpLeft = Animator.StringToHash("aimUpLeft");
    public static int aimRight = Animator.StringToHash("aimRight");
    public static int aimLeft = Animator.StringToHash("aimLeft");
    public static int aimDownRight = Animator.StringToHash("aimDownRight");
    public static int aimDownLeft = Animator.StringToHash("aimDownLeft");
    public static int isIdle = Animator.StringToHash("isIdle");
    public static int isMoving = Animator.StringToHash("isMove");
    public static int isAttack = Animator.StringToHash("isAttack");
    public static int isFaint = Animator.StringToHash("isFaint");
    #endregion

    public const float epsilon = 0.01f;
}
