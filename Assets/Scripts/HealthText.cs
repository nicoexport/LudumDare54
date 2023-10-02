using System;
using LudumDare.Assets.Scripts;
using TMPro;
using UnityEngine;

namespace LudumDare {
    public class HealthText : ComponentFeature<TextMeshProUGUI> {
        protected  void OnEnable() {
            Player.onHealthChanged += SetText;
        }

        protected void OnDisable() {
            Player.onHealthChanged -= SetText;
        }

        void SetText(int max, int current) {
            attachedComponent.text = $"{current}/{max}";
        }
    }
}
