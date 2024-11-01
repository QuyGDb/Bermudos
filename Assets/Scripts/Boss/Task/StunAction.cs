using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class StunAction : SkillsAction
    {

        private Poise poise;
        public float stunTime;
        public override void OnAwake()
        {

            base.OnAwake();
            poise = GetComponent<Poise>();
        }
        public override void OnStart()
        {
            stunTime = poise.stunTime;
        }
        public override TaskStatus OnUpdate()
        {
            Debug.Log("Stun" + Time.frameCount);
            if (poise.currentPoise <= 0 && stunTime > 0)
            {
                animator.SetTrigger(bossAnimationStateDic[bossAnimationState]);
                stunTime -= Time.deltaTime;
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
            poise.currentPoise = poise.maxPoise;
        }
    }
}