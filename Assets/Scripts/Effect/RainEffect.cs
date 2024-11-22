using UnityEngine;
using UnityEngine.SceneManagement;

public class RainEffect : MonoBehaviour
{
    private int appearanceRate;
    [SerializeField] private SoundEffectSO rainEffect;

    void Start()
    {
        gameObject.SetActive(false);
        appearanceRate = Random.Range(0, 3);
        if (appearanceRate == 0 || (SceneManager.GetSceneAt(0).name == "MainMenu"))
        {
            gameObject.SetActive(true);
            SoundEffectManager.Instance.PlaySoundEffectPersistent(rainEffect, true);
        }

    }
    private void OnDisable()
    {
        SoundEffectManager.Instance.StopSoundEffectLoop(rainEffect);
    }
}
