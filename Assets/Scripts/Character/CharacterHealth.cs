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
        public UnityEvent<int, int> onHealthChanged;

        protected void Start() {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount) {
            currentHealth -= amount;
            onHealthChanged.Invoke(maxHealth, currentHealth);
            if (currentHealth <= 0) {
                onDeath?.Invoke();
            }
        }
    }
}
