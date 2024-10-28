using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] protected Color flashColor;
    protected Rigidbody2D rb;
    public AnimationCurve damageFlashCurve;
    protected WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    [SerializeField] protected float damageForce;
    [SerializeField] protected float duration = 0.25f;
    [SerializeField] protected ParticleSystem stunEffect;
    [SerializeField] protected ParticleSystem bloodEffect;
    public void StunEffect(float stunTime)
    {
        ParticleSystem.MainModule mainModule = stunEffect.main;
        mainModule.startLifetimeMultiplier = stunTime;
        stunEffect.Play();

    }

}
