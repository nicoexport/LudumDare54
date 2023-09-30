using UnityEngine;

namespace LudumDare {
    public abstract class ComponentFeature<T> : MonoBehaviour where T : Component {
        [SerializeField]
        protected T attachedComponent;

        protected virtual void OnValidate() {
            if (!attachedComponent) {
                TryGetComponent(out attachedComponent);
            }
        }
    }
}
