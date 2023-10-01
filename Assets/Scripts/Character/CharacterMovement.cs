using MyBox;
using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LudumDare {
    public class CharacterMovement : ComponentFeature<Animator> {
        [SerializeField] CharacterController characterController;
        [SerializeField] float moveVelocity = 1f;

        public Vector2 velocity {
            get {
                return m_velocity;
            }
            private set {
                m_velocity = value;
                attachedComponent.SetFloat(nameof(velocity), value.magnitude);
                if (value.magnitude > 0.1f) {
                    forward = GetCardinal(value);
                }
            }
        }

        [field: SerializeField, ReadOnly]
        Vector2 m_velocity;
        [property: SerializeField]
        public Vector2 forward {
            get => new(forwardX, forwardY);
            private set {
                forwardX = value.x;
                forwardY = value.y;
            }
        }
        float forwardX {
            get => attachedComponent.GetFloat(nameof(forwardX));
            set => attachedComponent.SetFloat(nameof(forwardX), value);
        }
        float forwardY {
            get => attachedComponent.GetFloat(nameof(forwardY));
            set => attachedComponent.SetFloat(nameof(forwardY), value);
        }

        public void Move() => characterController.Move(velocity.SwizzleXY() * Time.deltaTime);

        protected void OnMove(InputValue value) {
            velocity = value.Get<Vector2>() * moveVelocity;
        }

        protected void OnMove(Vector2 value) {
            velocity = value * moveVelocity;
        }

        protected override void OnValidate() {
            base.OnValidate();
            if (!characterController) {
                TryGetComponent(out characterController);
            }
        }

        Vector2 GetCardinal(Vector2 inputVector) {
            inputVector = inputVector.normalized;
            Vector2[] cardinalVectors = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

            float maxDotProduct = Mathf.NegativeInfinity;
            var result = Vector2.zero;

            for (int i = 0; i < cardinalVectors.Length; i++) {
                float dotProduct = Vector2.Dot(inputVector, cardinalVectors[i]);

                if (dotProduct > maxDotProduct) {
                    maxDotProduct = dotProduct;
                    result = cardinalVectors[i];
                }
            }

            return result;
        }
    }
}
