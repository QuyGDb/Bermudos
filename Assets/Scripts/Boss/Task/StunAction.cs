using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class StunAction : Action
    {

        private Animator animator;
        private Poise poise;

        public override void OnAwake()
        {
            animator = GetComponent<Animator>();
            poise = GetComponent<Poise>();
        }


        public override TaskStatus OnUpdate()
        {
            if (poise.currentPoise <= 0)
            {
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
        }
    }
}