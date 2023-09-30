using System.Runtime.InteropServices.WindowsRuntime;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace LudumDare {
    public class CharacterAttack : ComponentFeature<Animator> {
        [SerializeField]
        Transform attackColliderTransform;
        [SerializeField]
        float attackColliderDistance = 0.5f;

        BoolBuffer intendBuffer = new();

        public bool intendsAttack {
            get => attachedComponent.GetBool(nameof(intendsAttack));
            set => attachedComponent.SetBool(nameof(intendsAttack), value);
        }

        Vector2 attackDir {
            get => new(attackDirX, attackDirY);
            set {
                attackDirX = value.x;
                attackDirY = value.y;
            }
        }

        public float attackDirX {
            get => attachedComponent.GetFloat(nameof(attackDirX));
            set => attachedComponent.SetFloat(nameof(attackDirX), value);
        }

        public float attackDirY {
            get => attachedComponent.GetFloat(nameof(attackDirY));
            set => attachedComponent.SetFloat(nameof(attackDirY), value);
        }

        protected void OnEnable() {
            DisableAttack();
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

        public void EnableAttack(Vector2 direction) {
            attackDir = direction;  
            attackColliderTransform.gameObject.SetActive(true);
            attackColliderTransform.position = transform.position + (direction.SwizzleXY() * attackColliderDistance);
        }

        public void DisableAttack() {
            attackColliderTransform.gameObject.SetActive(false);
        }
    }
}
