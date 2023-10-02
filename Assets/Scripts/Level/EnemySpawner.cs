using MyBox;
using UnityEngine;

namespace LudumDare.Assets.Scripts.Level {
    class EnemySpawner : MonoBehaviour {
        [SerializeField] GameObject[] enemyPrefabs;
        [SerializeField] int totalNumberOfEnemies;
        [SerializeField] MinMaxInt spawnDelayInFrames;
        

    }
}
