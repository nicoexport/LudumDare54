using UnityEngine;

namespace LudumDare.Assets {
    [CreateAssetMenu(menuName ="Game Config")]
    public class GameConfig : ScriptableObject {
        [field: SerializeField] public int playerMaxHealth { get; private set; } = 100;
    }
}
