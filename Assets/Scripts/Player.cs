using System;
using LudumDare.Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LudumDare.Assets.Scripts {
    public class Player : ComponentFeature<PlayerInput> {
        public static event Action<int, int> onHealthChanged;
        public static event Action onDeath;

        public static Player instance { get; private set; }

        protected void Awake() {
            if (instance != null && instance != this) {
                Destroy(this);
            } else {
                instance = this;
            }
        }

        protected void OnEnable() {
            EnemySpawner.onAllEnemiesKilled += DisableInput;
        }

        protected void OnDisable() {
            EnemySpawner.onAllEnemiesKilled -= DisableInput;
        }

        public void UpdateHealth(int maxHealth, int currentHealth) {
            onHealthChanged?.Invoke(maxHealth, currentHealth);
        }

        public void Die() {
            onDeath?.Invoke();
            var anim = GetComponent<Animator>();
            anim.SetBool("isFalling", true);
            var renderer = GetComponent<Renderer>();
            renderer.sortingLayerName = "Ground";
            var controller = GetComponent<CharacterController>();
            controller.enabled = false;
        }

        public void DisableInput() {
            attachedComponent.enabled = false;
        }
    }
}
