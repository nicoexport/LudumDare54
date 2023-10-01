using Slothsoft.UnityExtensions;
using UnityEngine;

namespace LudumDare.Assets.Scripts {
    public class FollowProjectile : BaseProjectile {
        [SerializeField]
        float moveSpeed;
        [SerializeField]
        float driftSpeed = 4;
        [SerializeField]
        int lifeTimeInFrames;
        [SerializeField]
        LayerMask targetLayers;
        [SerializeField]
        float detectionRange = 10f;



        BoolBuffer lifetimer = new();
        bool isInitialized;
        Vector2 moveDir;
        Transform target;

        public override void Shoot(Vector2 direction) {
            lifetimer.SetForFrames(lifeTimeInFrames);
            moveDir = direction.normalized;
            var candidates = Physics.OverlapSphere(transform.position, detectionRange, targetLayers);
            if (candidates.Length > 0) {
                target = candidates[0].transform;
            }
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

            if (target) {
                var targetDir = (target.position - transform.position).normalized;
                moveDir = Vector2.MoveTowards(moveDir, targetDir, driftSpeed);
            }

            transform.position += (moveSpeed * Time.deltaTime * moveDir).SwizzleXY();
        }

        public void Destroy() {
            Destroy(gameObject);
        }
    }
}
