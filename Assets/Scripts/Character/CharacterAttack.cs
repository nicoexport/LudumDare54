using UnityEngine;

namespace LudumDare {
    public class CharacterAttack : ComponentFeature<Animator> {
        BoolBuffer intendBuffer = new();

        public bool intendsAttack {
            get => attachedComponent.GetBool(nameof(intendsAttack));
            set => attachedComponent.SetBool(nameof(intendsAttack), value);
        }

        protected void Update() {
            intendsAttack = intendBuffer.value;
        }

        protected void FixedUpdate() {
            intendBuffer.Tick();
        }

        void OnAttack() {
            intendBuffer.SetForFrames(3);
        }
    }
}
