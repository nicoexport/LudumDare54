using Slothsoft.UnityExtensions;
using UnityEngine;

namespace LudumDare {
    class CharacterAttackState : ComponentStateBehaviour<CharacterAttack> {
        protected override void StateEnter(in AnimatorStateInfo stateInfo, int layerIndex) {
            if(!attachedAnimator.transform.TryGetComponentInChildren(out CharacterMovement movement)){
                return;
            }
            attachedComponent.EnableAttack(movement.forward);
        }
        protected override void StateExit(in AnimatorStateInfo stateInfo, int layerIndex) {
            attachedComponent.DisableAttack();
            
        }
        protected override void StateUpdate(AnimatorStateInfo stateInfo, int layerIndex) {
        }
    }
}
