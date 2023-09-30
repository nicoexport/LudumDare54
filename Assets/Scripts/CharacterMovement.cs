using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LudumDare {
    public class CharacterMovement : MonoBehaviour {
        [SerializeField] float moveVelocity = 1f;
        [SerializeField] CharacterController characterController;

        Vector2 velocity;

        protected void FixedUpdate() {
            characterController.Move(velocity.SwizzleXY() * Time.deltaTime);
        }

        protected void OnMove(InputValue value) {
            velocity = value.Get<Vector2>() * moveVelocity;
        }

        protected void OnValidate() {
            if (!characterController) {
                TryGetComponent(out characterController);
            }
        }
    }
}
