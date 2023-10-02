using System;
using MyBox;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LudumDare.Assets.Scripts.Level {
    class EnemySpawner : MonoBehaviour {
        public static event Action onAllEnemiesKilled;

        [SerializeField] GameObject[] enemyPrefabs;
        [SerializeField] int totalNumberOfEnemies;
        [SerializeField] MinMaxInt spawnDelayInFrames;
        [Separator]
        [SerializeField] Vector3[] spawnPositions;

        int spawnedEnemies;
        BoolBuffer timer = new();
        bool isSpawning;
        int killedEnemies;

        protected void Start() {
            StartSpawning();
        }

        void StartSpawning() {
            isSpawning = true;
            timer.SetForFrames(spawnDelayInFrames.Min);
        }

        protected void FixedUpdate() {
            if (!isSpawning) {
                return;
            }
            timer.Tick();
            if (!timer.value && spawnedEnemies < totalNumberOfEnemies) {
                Spawn();
            }
        }

        void Spawn() {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            var spawnPos = transform.position;
            if (spawnPositions.Length > 0) {
                int positionIndex = Random.Range(0, spawnPositions.Length);
                spawnPos = spawnPositions[positionIndex];
            }

            var enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPos, Quaternion.identity);
            spawnedEnemies++;
            timer.SetForFrames(spawnDelayInFrames.RandomInRangeInclusive());
            if(enemy.TryGetComponent(out CharacterHealth health)){
                health.onDeath.AddListener(CountKilledEnemies);
            }
        }

        void CountKilledEnemies() {
            killedEnemies++;
            if(killedEnemies == totalNumberOfEnemies) {
                onAllEnemiesKilled?.Invoke();
            }
        }

        protected void OnDrawGizmos() {
            Gizmos.color = Color.red;

            foreach (var pos in spawnPositions) {
                Gizmos.DrawWireSphere(pos, 1f);
            }
        }
    }
}
