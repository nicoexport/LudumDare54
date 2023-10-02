using MyBox;
using UnityEngine;

namespace LudumDare.Assets.Scripts.Level {
    class EnemySpawner : MonoBehaviour {
        [SerializeField] GameObject[] enemyPrefabs;
        [SerializeField] int totalNumberOfEnemies;
        [SerializeField] MinMaxInt spawnDelayInFrames;
        [Separator]
        [SerializeField] Vector3[] spawnPositions;

        protected void OnDrawGizmos() {
            Gizmos.color = Color.red;

            foreach (var pos in spawnPositions) {
                Gizmos.DrawWireSphere(pos, 1f);
            }
        }
    }
}
