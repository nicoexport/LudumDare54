using MyBox;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare {
    public class CharacterHealth : MonoBehaviour {
        [SerializeField]
        int maxHealth = 50;
        [SerializeField, ReadOnly]
        int currentHealth;

        public UnityEvent onDeath;

        protected void Start() {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount) {
            currentHealth -= amount;
            if (currentHealth <= 0) {
                onDeath?.Invoke();
            }
        }
    }
}
