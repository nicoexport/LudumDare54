using UnityEngine;

namespace LudumDare.Assets.Scripts {
    class HitBox : MonoBehaviour {
        [SerializeField] int damage = 10;
        [SerializeField] LayerMask whatIsEnemy;

        protected void OnTriggerEnter2D(Collider2D collider) {
            if (!whatIsEnemy.IsInLayerMask(collider.gameObject)) {
                Debug.Log($"not in layer {collider.gameObject.name}");
                return;
            }

            if(!collider.TryGetComponent(out CharacterHealth health)) {
                Debug.Log("no health component");
                return;
            }

            health.TakeDamage(damage);
        }
    }
}
