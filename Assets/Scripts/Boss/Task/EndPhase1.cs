using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPhase1 : Action
{
    private Animator animator;
    public SharedMaterial material;
    public float transitionTime = 1.0f;
    private float fadeAmount = 0.0f;
    private float transitionTime2;
    private RangedSkills rangedSkills;
    private MeleeSkills meleeSkills;
    private bool isEyeLoopDeath = false;
    [SerializeField] private SoundEffectSO transitionSoundEffect;
    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
        rangedSkills = GetComponent<RangedSkills>();
        meleeSkills = GetComponent<MeleeSkills>();
    }
    public override void OnStart()
    {
        SoundEffectManager.Instance.PlaySoundEffect(transitionSoundEffect);
        rangedSkills.StopAllCoroutines();
        meleeSkills.StopAllCoroutines();
        transitionTime2 = transitionTime;
        StartCoroutine(EndPhase1Coroutine());
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    public override TaskStatus OnUpdate()
    {

        if (transitionTime > 0 && isEyeLoopDeath)
        {
            fadeAmount = fadeAmount + (Time.deltaTime / transitionTime2);
            transitionTime -= Time.deltaTime;
            material.Value.SetFloat("_FadeAmount", fadeAmount);
            return TaskStatus.Running;
        }
        if (!isEyeLoopDeath)
        {
            return TaskStatus.Running;
        }
        return TaskStatus.Success;
    }
    public IEnumerator EndPhase1Coroutine()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger(Settings.EyeLoopDeath);
        isEyeLoopDeath = true;
    }

}
