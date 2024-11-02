using UnityEngine;
using UnityEngine.Assertions.Must;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class StunAction : SkillsAction
    {
        private Boss boss;
        private Poise poise;
        public SharedFloat stunTime;
        public override void OnAwake()
        {

            base.OnAwake();
            poise = GetComponent<Poise>();
            boss = GetComponent<Boss>();
        }
        public override void OnStart()
        {
            //stunTime = poise.stunTime;
        }
        public override TaskStatus OnUpdate()
        {
            if (poise.currentPoise <= 0 && stunTime.Value > 0)
            {
                animator.SetTrigger(bossAnimationStateDic[bossAnimationState]);
                stunTime.Value -= Time.deltaTime;
                return TaskStatus.Running;
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 <= 0.0001f)
                {
                    if (boss.isPhaseTwo)
                        animator.SetTrigger(Settings.Idle2);
                    else
                        animator.SetTrigger(Settings.Idle);
                    return TaskStatus.Running;
                }
                return TaskStatus.Success;
            }
        }

        public override void OnEnd()
        {
            if (poise.currentPoise <= 0)
                poise.currentPoise = poise.maxPoise;
            if (stunTime.Value <= 0)
                stunTime = poise.stunTime;
        }
    }
}