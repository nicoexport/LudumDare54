using LudumDare.Assets.Scripts;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace LudumDare {
    public class CharacterRangedAttack : ComponentFeature<Animator> {
        [SerializeField]
        BaseProjectile projectilePrefab;
        [SerializeField] float spawnDistance = 0.5f;

        BoolBuffer intendBuffer = new();

        public bool intendsRangedAttack {
            get => attachedComponent.GetBool(nameof(intendsRangedAttack));
            set => attachedComponent.SetBool(nameof(intendsRangedAttack), value);
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

        public void Attack(Vector2 direction) {
            attackDir = direction;
            var projectile = Instantiate(projectilePrefab, transform.position + (direction * spawnDistance).SwizzleXY(), Quaternion.identity);
            projectile.Shoot(direction);
        }

        protected void Update() {
            intendsRangedAttack = intendBuffer.value;
        }

        protected void FixedUpdate() {
            intendBuffer.Tick();
        }

        void OnRangedAttack() {
            intendBuffer.SetForFrames(3);
        }
    }
}
