using UnityEngine;

namespace LudumDare.Assets.Scripts {
    public class Player : MonoBehaviour {
        public static Player instance { get; private set; }
        protected void Awake() {

            if (instance != null && instance != this) {
                Destroy(this);
            } else {
                instance = this;
            }
        }
    }
}
