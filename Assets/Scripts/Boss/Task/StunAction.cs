using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class StunAction : SkillsAction
    {

        private Poise poise;
        public float stunTime;
        public override void OnAwake()
        {
            stunTime = poise.stunTime;
            base.OnAwake();
            poise = GetComponent<Poise>();
        }

        public override TaskStatus OnUpdate()
        {
            if (poise.currentPoise <= 0 && poise.stunTime > 0)
            {
                animator.SetTrigger(bossAnimationStateDic[bossAnimationState]);
                poise.stunTime -= Time.deltaTime;
                return TaskStatus.Running;
            }
            else
            {
                return TaskStatus.Success;
            }
        }

        public override void OnEnd()
        {
            if (poise.currentPoise <= 0)
                poise.currentPoise = poise.maxPoise;
            if (stunTime <= 0)
                stunTime = poise.stunTime;
        }
    }
}