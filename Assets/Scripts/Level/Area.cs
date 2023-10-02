using LudumDare.Assets.Scripts;
using UnityEngine;

namespace LudumDare {
    public class Area : MonoBehaviour {
        [SerializeField]
        SpriteRenderer platform;

        [SerializeField]
        int Width, Height;

        bool isWalkable = true;

        public bool hasPlayer { get; private set; } = false;

        protected void Awake() {
            SetScale();
        }

        protected void OnValidate() {
            Awake();
        }

        void SetScale() {
            transform.localScale = new Vector3(Width, Height);
        }

        protected void OnTriggerStay(Collider other) {
            if (!other.CompareTag("Player")) {
                return;
            }
            hasPlayer = true;
            if (!isWalkable) {
                // Implement falling
                Player.instance.Die();
            }
        }

        protected void OnTriggerExit(Collider other) {
            if (!other.CompareTag("Player")) {
                return;
            }
            hasPlayer = false;
        }

        public int GetWidth() {
            return Width;
        }

        public int GetHeight() {
            return Height;
        }

        public bool GetIsWalkable() {
            return isWalkable;
        }

        public void SetIsWalkable(bool isWalkable) {
            this.isWalkable = isWalkable;
            platform.gameObject.SetActive(isWalkable);
        }

        [ContextMenu("Set Walkable")]
        public void SetWalkable() {
            SetIsWalkable(true);
        }

        [ContextMenu("Set !Walkable")]
        public void SetNonWalkable() {
            SetIsWalkable(false);
        }
    }
}
