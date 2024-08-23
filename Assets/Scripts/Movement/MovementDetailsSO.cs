using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementDetails_", menuName = "ScriptableObjects/Movement/MovementDetails")]
public class MovementDetailsSO : ScriptableObject
{
    #region Header MOVEMENT DETAILS
    [Space(10)]
    [Header("MOVEMENT DETAILS")]
    #endregion Header
    #region Tooltip
    [Tooltip("The minimum move speed. The GetMoveSpeed method calculates a random value between the minimum and maximum")]
    #endregion Tooltip
    public float minMoveSpeed = 8f;
    #region Tooltip
    [Tooltip("The maximum move speed. The GetMoveSpeed method calculates a random value between the minimum and maximum")]
    #endregion Tooltip
    public float maxMoveSpeed = 8f;
    #region Tooltip
    [Tooltip("If there is a roll movement- this is the roll speed")]
    #endregion
    public float dashSpeed; // for player
    #region Tooltip
    [Tooltip("If there is a roll movement - this is the roll distance")]
    #endregion
    public float dashDistance; // for player
    #region Tooltip
    [Tooltip("If there is a roll movement - this is the cooldown time in seconds between roll actions")]
    #endregion
    public float dashCooldownTime; // for player

    /// <summary>
    /// Get a random movement speed between the minimum and maximum values
    /// </summary>
    public float GetMoveSpeed()
    {
        if (minMoveSpeed == maxMoveSpeed)
        {
            return minMoveSpeed;
        }
        else
        {
            return Random.Range(minMoveSpeed, maxMoveSpeed);
        }
    }

    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveRange(this, nameof(minMoveSpeed), minMoveSpeed, nameof(maxMoveSpeed), maxMoveSpeed, false);

        if (dashDistance != 0f || dashSpeed != 0 || dashCooldownTime != 0)
        {
            HelperUtilities.ValidateCheckPositiveValue(this, nameof(dashDistance), dashDistance, false);
            HelperUtilities.ValidateCheckPositiveValue(this, nameof(dashSpeed), dashSpeed, false);
            HelperUtilities.ValidateCheckPositiveValue(this, nameof(dashCooldownTime), dashCooldownTime, false);
        }

    }

#endif
    #endregion Validation
}
