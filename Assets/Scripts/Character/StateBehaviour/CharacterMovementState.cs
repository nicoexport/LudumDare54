using UnityEngine;

namespace LudumDare {
    class CharacterMovementState : ComponentStateBehaviour<CharacterMovement> {
        protected override void StateEnter(in AnimatorStateInfo stateInfo, int layerIndex) {
            throw new System.NotImplementedException();
        }

        protected override void StateExit(in AnimatorStateInfo stateInfo, int layerIndex) {
            throw new System.NotImplementedException();
        }

        protected override void StateUpdate(AnimatorStateInfo stateInfo, int layerIndex) => throw new System.NotImplementedException();
    }
}
