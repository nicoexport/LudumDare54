using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudumDare {
    public class Area : MonoBehaviour {
        [SerializeField]
        SpriteRenderer platform;

        [SerializeField]
        int Width, Height;

        bool isWalkable = true; 

        protected void Awake() {
            SetScale();
        }

        protected void OnValidate() {
            Awake();
        }

        void SetScale() {
            transform.localScale = new Vector3(Width, Height);
        }

        protected void OnTriggerEnter(Collider other) {
            if (isWalkable) {
                return;
            }

            if (other.CompareTag("Player")) {
                // Implement falling
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public int GetWidth() {
            return Width;
        }

        public int GetHeight() {
            return Height;
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
