using UnityEngine;
using UnityEngine.Events;

namespace LudumDare.Assets {
    class DestroyAfterDelay : MonoBehaviour {
        public UnityEvent onObjectDestroyed;

        [SerializeField] int delayInFrames = 75;
        BoolBuffer timer = new();

        protected void OnEnable() {
            timer.SetForFrames(delayInFrames);
        }

        protected void FixedUpdate() {
            timer.Tick();
            if (!timer.value) {
                onObjectDestroyed.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
