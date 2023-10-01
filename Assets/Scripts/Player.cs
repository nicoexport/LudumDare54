using System;
using UnityEngine;

namespace LudumDare.Assets.Scripts {
    public class Player : MonoBehaviour {
        public static event Action<int, int> onHealthChanged;

        public static Player instance { get; private set; }

        public void UpdateHealth(int maxHealth, int currentHealth) {
            onHealthChanged?.Invoke(maxHealth, currentHealth);
        }

        protected void Awake() {
            if (instance != null && instance != this) {
                Destroy(this);
            } else {
                instance = this;
            }
        }
    }
}
