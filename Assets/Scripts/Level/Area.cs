using UnityEngine;

namespace LudumDare {
    public class Area : MonoBehaviour {
        [SerializeField]
        SpriteRenderer Ground, FrontBorder;

        [SerializeField]
        int Width, Height;

        bool isWalkable = true; 

        protected void Awake() {
            SetScale();
        }

        void SetScale() {
            transform.localScale = new Vector3(Width, Height);
        }

        protected void OnTriggerEnter2D(Collider2D other) {

        }

        public int GetWidth() {
            return Width;
        }

        public int GetHeight() {
            return Height;
        }

        public void SetIsWalkable(bool isWalkable) {
            this.isWalkable = isWalkable;
        }

        public void SetOrderInLayer(int layer) {
            Ground.sortingOrder = layer;
            FrontBorder.sortingOrder = layer;
        }
    }
}
