using MyBox;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace LudumDare.Assets.Scripts {
    public class GameConfigManager : Singleton<GameConfigManager> {
        [field: SerializeField, Expandable]
        public GameConfig gameConfig { get; private set; }
    }
}
