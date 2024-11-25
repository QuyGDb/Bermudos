using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SoundEffectManager : SingletonMonobehaviourPersistent<SoundEffectManager>
{
    public float soundsVolume = 10;
    List<SoundEffect> soundEffects = new List<SoundEffect>();
    private void Start()
    {
        if (PlayerPrefs.HasKey("soundsVolume"))
        {
            soundsVolume = PlayerPrefs.GetFloat("soundsVolume");
        }

        SetSoundsVolume(soundsVolume);
    }


    /// <summary>
    /// Play the sound effect
    /// </summary>
    public void PlaySoundEffect(SoundEffectSO soundEffect)
    {
        // Play sound using a sound gameobject and component from the object pool
        SoundEffect sound = (SoundEffect)PoolManager.Instance.ReuseComponent(soundEffect.soundPrefab, Vector3.zero, Quaternion.identity);
        sound.SetSound(soundEffect);
        sound.gameObject.SetActive(true);
        StartCoroutine(DisableSound(sound, soundEffect.soundEffectClip.length));

    }
    public void PlaySoundEffectPersistent(SoundEffectSO soundEffect, bool isLoop)
    {

        var loopSound = (SoundEffect)PoolManager.Instance.ReuseComponent(soundEffect.soundPrefab, Vector3.zero, Quaternion.identity);
        loopSound.SetSound(soundEffect, isLoop);
        loopSound.gameObject.SetActive(true);
        soundEffects.Add(loopSound);
    }

    public void StopSoundEffectLoop(SoundEffectSO soundEffect)
    {
        foreach (var sound in soundEffects)
        {
            if (sound.soundEffect == soundEffect)
            {
                sound.gameObject.SetActive(false);
                soundEffects.Remove(sound);
                return;
            }
        }
    }
    /// <summary>
    /// Disable sound effect object after it has played thus returning it to the object pool
    /// </summary>
    private IEnumerator DisableSound(SoundEffect sound, float soundDuration)
    {
        yield return new WaitForSeconds(soundDuration);
        sound.gameObject.SetActive(false);
    }

    //
    /// <summary>
    /// Change sounds volume
    /// </summary>
    public void ChangeSoundsVolume(float soundsVolume)
    {
        int maxSoundsVolume = 20;

        if (soundsVolume >= maxSoundsVolume) return;

        this.soundsVolume = soundsVolume;
        SetSoundsVolume(soundsVolume); ;
    }


    /// <summary>
    /// Set sounds volume
    /// </summary>
    private void SetSoundsVolume(float soundsVolume)
    {
        float muteDecibels = -80f;

        if (soundsVolume == 0)
        {
            GameResources.Instance.soundsMasterMixerGroup.audioMixer.SetFloat("soundsVolume", muteDecibels);
        }
        else
        {
            GameResources.Instance.soundsMasterMixerGroup.audioMixer.SetFloat("soundsVolume", HelperUtilities.LinearToDecibels(soundsVolume));
        }
    }
}