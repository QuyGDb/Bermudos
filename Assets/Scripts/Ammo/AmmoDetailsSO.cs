using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoDetailsSO_", menuName = "ScriptableObjects/AmmoDetailsSO", order = 1)]
public class AmmoDetailsSO : ScriptableObject
{
    public float maxSpeed;
    public int damage;
    public float trajectoryMaxHeight;
    public AnimationClip enemyAmmoType;
    public Color ammoEffectType;
    public GameObject ammoPrefab;

    #region Curve
    public AnimationCurve trajectoryAnimationCurve;
    public AnimationCurve axisCorrectionAnimationCurve;
    public AnimationCurve ammoSpeedAnimationCurve;
    #endregion

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(maxSpeed), maxSpeed, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(damage), damage, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(trajectoryMaxHeight), trajectoryMaxHeight, true);
        HelperUtilities.ValidateCheckNullValue(this, nameof(enemyAmmoType), enemyAmmoType);
        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoPrefab), ammoPrefab);
    }
#endif
    #endregion
}
