using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class MusicManager : SingletonMonobehaviourPersistent<MusicManager>
{
    private AudioSource musicAudioSource = null;
    private AudioClip currentAudioClip = null;
    private Coroutine fadeOutMusicCoroutine;
    private Coroutine fadeInMusicCoroutine;
    public float musicVolume = 10;
    bool isPlayingFirstClip = true;
    protected override void Awake()
    {
        base.Awake();

        // Load components
        musicAudioSource = GetComponent<AudioSource>();

        // Start with music off
        GameResources.Instance.musicOffSnapshot.TransitionTo(0f);

    }

    private void Start()
    {
        // Check if volume levels have been saved in playerprefs - if so retrieve and set them
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
        }

        SetMusicVolume(musicVolume);

        PlayBackgroundMusic();
    }

    private void OnDisable()
    {
        // Save volume settings in playerprefs
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }
    public void PlayBackgroundMusic()
    {
        if (isPlayingFirstClip)
        {
            PlayMusic(GameResources.Instance.musicTrack1);
            isPlayingFirstClip = false;
            Invoke("PlayerBackgroundMusic", GameResources.Instance.musicTrack1.musicClip.length);
        }
        else
        {
            PlayMusic(GameResources.Instance.musicTrack2);
            isPlayingFirstClip = true;
            Invoke("PlayerBackgroundMusic", GameResources.Instance.musicTrack2.musicClip.length);
        }
    }
    public void StopPlayingBackgroundMusic()
    {
        StopMusic();
        CancelInvoke("PlayBackgroundMusic");
    }

    public void PlayMusic(MusicTrackSO musicTrack, float fadeOutTime = Settings.musicFadeOutTime, float fadeInTime = Settings.musicFadeInTime)
    {
        // Play music track
        StartCoroutine(PlayMusicRoutine(musicTrack, fadeOutTime, fadeInTime));
    }
    public void StopMusic()
    {
        musicAudioSource.Stop();
    }


    /// <summary>
    /// Play music for room routine
    /// </summary>
    private IEnumerator PlayMusicRoutine(MusicTrackSO musicTrack, float fadeOutTime, float fadeInTime)
    {
        // if fade out routine already running then stop it
        if (fadeOutMusicCoroutine != null)
        {
            StopCoroutine(fadeOutMusicCoroutine);
        }

        // if fade in routine already running then stop it
        if (fadeInMusicCoroutine != null)
        {
            StopCoroutine(fadeInMusicCoroutine);
        }

        // If the music track has changed then play new music track
        if (musicTrack.musicClip != currentAudioClip)
        {
            currentAudioClip = musicTrack.musicClip;

            yield return fadeOutMusicCoroutine = StartCoroutine(FadeOutMusic(fadeOutTime));

            yield return fadeInMusicCoroutine = StartCoroutine(FadeInMusic(musicTrack, fadeInTime));
        }

        yield return null;
    }

    /// <summary>
    /// Fade out music routine
    /// </summary>
    private IEnumerator FadeOutMusic(float fadeOutTime)
    {
        GameResources.Instance.musicLowSnapshot.TransitionTo(fadeOutTime);

        yield return new WaitForSeconds(fadeOutTime);
    }

    /// <summary>
    /// Fade in music routine
    /// </summary>
    private IEnumerator FadeInMusic(MusicTrackSO musicTrack, float fadeInTime)
    {
        // Set clip & play
        musicAudioSource.clip = musicTrack.musicClip;
        musicAudioSource.volume = musicTrack.musicVolume;
        musicAudioSource.Play();

        GameResources.Instance.musicOnFullSnapshot.TransitionTo(fadeInTime);

        yield return new WaitForSeconds(fadeInTime);
    }

    /// <summary>
    /// Change music volume
    /// </summary>
    public void ChangeMusicVolume(float musicVolume)
    {
        int maxMusicVolume = 20;
        if (musicVolume >= maxMusicVolume) return;
        this.musicVolume = musicVolume;
        SetMusicVolume(musicVolume);
    }



    /// <summary>
    /// Set music volume
    /// </summary>
    public void SetMusicVolume(float musicVolume)
    {
        float muteDecibels = -80f;

        if (musicVolume == 0)
        {
            GameResources.Instance.musicMasterMixerGroup.audioMixer.SetFloat("musicVolume", muteDecibels);
        }
        else
        {
            GameResources.Instance.musicMasterMixerGroup.audioMixer.SetFloat("musicVolume", HelperUtilities.LinearToDecibels(musicVolume));
        }
    }
}