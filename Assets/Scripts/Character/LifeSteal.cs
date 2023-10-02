using UnityEngine;

namespace LudumDare.Assets.Scripts.Character {
    class LifeSteal : MonoBehaviour {
        [SerializeField] CharacterHealth health;
        [SerializeField, Range(0f, 1f)] float percent;

        public void Steal(int amount) {
            health.GainHealth((int)(amount * percent));
        }

    }
}
