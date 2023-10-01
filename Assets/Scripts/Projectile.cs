using Slothsoft.UnityExtensions;
using UnityEngine;

namespace LudumDare.Assets.Scripts {
    public class Projectile : MonoBehaviour {
        [SerializeField] int lifeTimeInFrames;
        [SerializeField] float moveSpeed = 2f;
        BoolBuffer lifetimer = new();
        bool isInitialized;
        Vector2 moveDir;

        public void Shoot(Vector2 direction) {
            lifetimer.SetForFrames(lifeTimeInFrames);
            moveDir = direction.normalized;
            isInitialized = true;
        }

        protected void FixedUpdate() {
            if (!isInitialized) {
                return;
            }

            lifetimer.Tick();
            if (!lifetimer.value) {
                Destroy(gameObject);
            }

            transform.position += (moveSpeed * Time.deltaTime * moveDir).SwizzleXY();
        }

        public void Destroy() {
            Destroy(gameObject);
        }
    }
}
