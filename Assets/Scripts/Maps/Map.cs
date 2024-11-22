using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    [SerializeField] private string mapName;
    private float repeatInterval = 10f;
    [SerializeField] private List<Enviroment> enviroments;
    private void Start()
    {
        StaticEventHandler.CallMapChangedEvent(this);
        foreach (var enviroment in enviroments)
        {
            if (enviroment == Enviroment.Water)
            {
                InvokeRepeating(nameof(PlayFlowingWaterSoundEffect), 0, repeatInterval);
            }
            if (enviroment == Enviroment.Sea)
            {
                SoundEffectManager.Instance.PlaySoundEffectPersistent(GameResources.Instance.waweOceanEffect, true);
            }
        }

    }
    private void OnDisable()
    {
        CancelInvoke(nameof(PlayFlowingWaterSoundEffect));
        SoundEffectManager.Instance.StopSoundEffectLoop(GameResources.Instance.waweOceanEffect);
    }
    private void PlayFlowingWaterSoundEffect()
    {
        SoundEffectManager.Instance.PlaySoundEffect(GameResources.Instance.flowingWater);
    }

    public int GetBossState()
    {
        if (PlayerPrefs.HasKey("isBossDefeated"))
        {
            return PlayerPrefs.GetInt("isBossDefeated");
        }
        else
        {
            return 0;
        }
    }

}
