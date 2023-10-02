using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ThrownAway.Runtime {
    public static class GameInitializer {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InitializeGame() {
            var game = Object.Instantiate(Resources.Load<GameObject>("P_Game"));
            if (!game) {
                throw new ApplicationException();
            }

            Object.DontDestroyOnLoad(game);
        }
    }
}