using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private float normalizedValue = 20f;

    private void Start()
    {
        StartCoroutine(SetVolumeCoroutine());
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.AddListener((value) => MusicManager.Instance.ChangeMusicVolume(value * normalizedValue));
        sfxSlider.onValueChanged.AddListener((value) => SoundEffectManager.Instance.ChangeSoundsVolume(value * normalizedValue));
    }
    private IEnumerator SetVolumeCoroutine()
    {
        yield return null;
        musicSlider.value = MusicManager.Instance.musicVolume / normalizedValue;
        sfxSlider.value = SoundEffectManager.Instance.soundsVolume / normalizedValue;
    }
    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(musicSlider), musicSlider);
        HelperUtilities.ValidateCheckNullValue(this, nameof(sfxSlider), sfxSlider);
    }
#endif
    #endregion
}
