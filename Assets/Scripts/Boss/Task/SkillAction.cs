using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHitSkillsAction : Action
{
    [HideInInspector] public Animator animator;

    public BossAnimationState bossAnimationState;
    public int normalizedTime;
    private bool isFirstUpdate;
    // Animator parameters - Boss
    private int isPhase2 = Animator.StringToHash("isPhase2");
    private int Idle = Animator.StringToHash("Idle");
    private int ouch = Animator.StringToHash("ouch");
    private int OpenMouth = Animator.StringToHash("OpenMouth");
    private int eyeAttack = Animator.StringToHash("eyeAttack");
    private int eyeLoop = Animator.StringToHash("eyeLoop");
    private int Hop = Animator.StringToHash("Hop");
    private int EyeLoopDeath = Animator.StringToHash("EyeLoopDeath");
    private int Base = Animator.StringToHash("Base");
    private int Walk = Animator.StringToHash("Walk");
    private int swipe = Animator.StringToHash("swipe");
    private int mouthOpenLoop = Animator.StringToHash("mouthOpenLoop");
    private int spearAtk = Animator.StringToHash("spearAtk");
    private int spinny = Animator.StringToHash("spinny");

    private Dictionary<BossAnimationState, int> bossAnimationStateDic = new();

    public override void OnAwake()
    {
        bossAnimationStateDic.Add(BossAnimationState.isPhase2, isPhase2);
        bossAnimationStateDic.Add(BossAnimationState.Idle, Idle);
        bossAnimationStateDic.Add(BossAnimationState.ouch, ouch);
        bossAnimationStateDic.Add(BossAnimationState.OpenMouth, OpenMouth);
        bossAnimationStateDic.Add(BossAnimationState.eyeAttack, eyeAttack);
        bossAnimationStateDic.Add(BossAnimationState.eyeLoop, eyeLoop);
        bossAnimationStateDic.Add(BossAnimationState.Hop, Hop);
        bossAnimationStateDic.Add(BossAnimationState.EyeLoopDeath, EyeLoopDeath);
        bossAnimationStateDic.Add(BossAnimationState.Base, Base);
        bossAnimationStateDic.Add(BossAnimationState.Walk, Walk);
        bossAnimationStateDic.Add(BossAnimationState.swipe, swipe);
        bossAnimationStateDic.Add(BossAnimationState.mouthOpenLoop, mouthOpenLoop);
        bossAnimationStateDic.Add(BossAnimationState.spearAtk, spearAtk);
        bossAnimationStateDic.Add(BossAnimationState.spinny, spinny);


        animator = GetComponent<Animator>();
    }
    public override void OnStart()
    {
        animator.SetTrigger(bossAnimationStateDic[bossAnimationState]);
        isFirstUpdate = true;
    }

    public virtual TaskStatus HandleTimeOfHits(int time)
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            return TaskStatus.Running;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= time)
            return TaskStatus.Running;
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        isFirstUpdate = false;
    }
}
