using System;
using System.Collections;
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
        [SerializeField] GameObject spawnEffectPrefab;

        int spawnedEnemies;
        BoolBuffer timer = new();
        bool tickTimers;
        int killedEnemies;

        protected void OnEnable() {
            Player.onDeath += StopSpawning;
        }

        protected void OnDisable() {
            Player.onDeath -= StopSpawning;
        }

        protected void Start() {
            StartSpawning();
        }

        void StartSpawning() {
            tickTimers = true;
            timer.SetForFrames(spawnDelayInFrames.Min);
        }

        protected void FixedUpdate() {
            if (!tickTimers) {
                return;
            }
            timer.Tick();
            if (!timer.value && spawnedEnemies < totalNumberOfEnemies) {
                StartCoroutine(Spawn());
            }
        }

        IEnumerator Spawn() {
            tickTimers = false;
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            var spawnPos = transform.position;
            if (spawnPositions.Length > 0) {
                int positionIndex = Random.Range(0, spawnPositions.Length);
                spawnPos = spawnPositions[positionIndex];
            }
            var spawnEffect = Instantiate(spawnEffectPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);

            Debug.Log("Spawn enemy");
            var enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPos, Quaternion.identity);
            spawnedEnemies++;
            if (enemy.TryGetComponent(out CharacterHealth health)) {
                health.onDeath.AddListener(CountKilledEnemies);
            }
            timer.SetForFrames(spawnDelayInFrames.RandomInRangeInclusive());
            tickTimers = true;
        }

        void CountKilledEnemies() {
            killedEnemies++;
            if (killedEnemies == totalNumberOfEnemies) {
                onAllEnemiesKilled?.Invoke();
            }
        }

        void StopSpawning() {
            tickTimers = false;
        }

        protected void OnDrawGizmos() {
            Gizmos.color = Color.red;

            foreach (var pos in spawnPositions) {
                Gizmos.DrawWireSphere(pos, 1f);
            }
        }
    }
}
