using MyBox;
using UnityEngine;

namespace LudumDare {
    public class CharacterHealth : MonoBehaviour {
        [SerializeField]
        int maxHealth = 50;
        [SerializeField, ReadOnly]
        int currentHealth;

        protected void Start() {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount) {
            currentHealth -= amount;
            if (currentHealth <= 0){
                Destroy(gameObject);
            }
        }
    }
}
