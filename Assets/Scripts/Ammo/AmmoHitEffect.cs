using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AmmoHitEffect : MonoBehaviour
{
    private ParticleSystem particleSystemHitEffect;
    private void Awake()
    {
        particleSystemHitEffect = GetComponent<ParticleSystem>();
    }

    public void InitialiseAmmoHitEffect(Color ammoHitEffectType)
    {
        ParticleSystem.MainModule main = particleSystemHitEffect.main;
        main.startColor = ammoHitEffectType;
        gameObject.SetActive(true);
    }
}
