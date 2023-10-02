using MyBox;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare {
    public class CharacterHealth : MonoBehaviour {
        [SerializeField]
        int maxHealth = 50;
        [SerializeField, ReadOnly]
        int currentHealth;
        [SerializeField]
        int invincabilityFrames = 0;

        BoolBuffer invuTimer = new();

        public UnityEvent onDeath;
        public UnityEvent<int, int> onHealthChanged;
        public UnityEvent onDamageTaken;

        protected void Start() {
            currentHealth = maxHealth;
            onHealthChanged?.Invoke(maxHealth, currentHealth);
        }

        protected void FixedUpdate() {
            invuTimer.Tick();
        }

        public void GainHealth(int amount) {
            currentHealth += amount;
            if (currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }
            onHealthChanged.Invoke(maxHealth, currentHealth);
        }

        public void TakeDamage(int amount) {
            if (invuTimer.value) {
                return;
            }
            invuTimer.SetForFrames(invincabilityFrames);    
            currentHealth -= amount;
            onHealthChanged.Invoke(maxHealth, currentHealth);
            onDamageTaken.Invoke();
            if (currentHealth <= 0) {
                onDeath?.Invoke();
            }
        }
    }
}
