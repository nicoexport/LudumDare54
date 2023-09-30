using UnityEngine;
using UnityEngine.Assertions;

namespace LudumDare {
    public abstract class ComponentStateBehaviour<T> : StateMachineBehaviour where T : Component {
        protected Animator attachedAnimator { get; private set; }
        protected T attachedComponent { get; private set; }

        public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) {
            attachedAnimator = animator;
            animator.TryGetComponent(out T component);
            attachedComponent = component;

            Assert.IsTrue(attachedComponent, $"Animator {animator} does not have a {typeof(T)} component!");

            StateEnter(stateInfo, layerIndex);
        }

        public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) =>
            StateExit(stateInfo, layerIndex);

        public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) {
            if (animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash == stateInfo.fullPathHash) {
                StateUpdate(stateInfo, layerIndex);
            }
        }

        protected abstract void StateEnter(in AnimatorStateInfo stateInfo, int layerIndex);

        protected abstract void StateUpdate(AnimatorStateInfo stateInfo, int layerIndex);

        protected abstract void StateExit(in AnimatorStateInfo stateInfo, int layerIndex);
    }
}