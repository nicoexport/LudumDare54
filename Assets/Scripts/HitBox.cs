using UnityEngine;
using UnityEngine.Events;

namespace LudumDare.Assets.Scripts {
    class HitBox : MonoBehaviour {
        [SerializeField] int damage = 10;
        [SerializeField] LayerMask whatIsEnemy;
        [SerializeField] UnityEvent onHit;

        protected void OnTriggerEnter(Collider collider) {
            if (!whatIsEnemy.IsInLayerMask(collider.gameObject)) {
                return;
            }

            if (!collider.TryGetComponent(out CharacterHealth health)) {
                return;
            }

            health.TakeDamage(damage);
            onHit.Invoke();
        }
    }
}
