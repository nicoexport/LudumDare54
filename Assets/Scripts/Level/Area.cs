using UnityEngine;

namespace LudumDare {
    public class Area : MonoBehaviour {
        [SerializeField]
        GameObject Ground, FrontBorder;

        [SerializeField]
        int Width, Height;

        protected void Awake() {
            Ground = GameObject.Find("Ground");
            FrontBorder = GameObject.Find("FrontBorder");
            SetFrontBorder(false);
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

        public void SetFrontBorder(bool isVisible) {
            FrontBorder.SetActive(isVisible);
        }
    }
}
