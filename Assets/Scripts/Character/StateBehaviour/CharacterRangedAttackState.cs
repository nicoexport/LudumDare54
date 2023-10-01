using Slothsoft.UnityExtensions;
using UnityEngine;

namespace LudumDare {
    class CharacterRangedAttackState : ComponentStateBehaviour<CharacterRangedAttack> {
        protected override void StateEnter(in AnimatorStateInfo stateInfo, int layerIndex) {
            if (!attachedAnimator.transform.TryGetComponentInChildren(out CharacterMovement movement)) {
                return;
            }
            attachedComponent.Attack(movement.forward);
        }
        protected override void StateExit(in AnimatorStateInfo stateInfo, int layerIndex) {


        }
        protected override void StateUpdate(AnimatorStateInfo stateInfo, int layerIndex) {
        }
    }
}
