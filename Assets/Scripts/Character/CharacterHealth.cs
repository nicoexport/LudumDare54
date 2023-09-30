using UnityEngine;

namespace LudumDare {
    public class CharacterHealth : MonoBehaviour {
        public void TakeDamage(int amount) {
            Debug.Log($"i took {amount} Damage!");
        }
    }
}
