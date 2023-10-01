using UnityEngine;

namespace LudumDare.Assets.Scripts.Level {
    public class SpriteRendererOrderSetter : ComponentFeature<SpriteRenderer> {
        [SerializeField] bool applyDuringRuntime;

        protected void Start() {
            attachedComponent.sortingOrder = -(int)(transform.position.y * 100f);
        }

        protected void FixedUpdate() {
            if (applyDuringRuntime) {
                Start();
            }
        }

        protected override void OnValidate() {
            base.OnValidate();
            Start();
        }
    }
}
