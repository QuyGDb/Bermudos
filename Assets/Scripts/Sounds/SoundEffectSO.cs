using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffect_", menuName = "ScriptableObjects/Sounds/SoundEffect")]
public class SoundEffectSO : ScriptableObject
{
    #region Header SOUND EFFECT DETAILS
    [Space(10)]
    [Header("SOUND EFFECT DETAILS")]
    #endregion
    #region Tooltip
    [Tooltip("The name for the sound effect")]
    #endregion
    public string soundEffectName;
    #region Tooltip
    [Tooltip("The prefab for the sound effect")]
    #endregion
    public GameObject soundPrefab;
    #region Tooltip
    [Tooltip("The audio clip for the sound effect")]
    #endregion
    public AudioClip soundEffectClip;

    #region Tooltip
    [Tooltip("The sound effect volume.")]
    #endregion
    [Range(0f, 1f)]
    public float soundEffectVolume = 1f;
    [Range(0f, 5f)]
    public float soundEffectPitch = 1f;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(soundEffectName), soundEffectName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(soundPrefab), soundPrefab);
        HelperUtilities.ValidateCheckNullValue(this, nameof(soundEffectClip), soundEffectClip);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(soundEffectVolume), soundEffectVolume, true);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(soundEffectPitch), soundEffectPitch, true);
    }
#endif
    #endregion
}
