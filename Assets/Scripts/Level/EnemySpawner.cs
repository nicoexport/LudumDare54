using MyBox;
using UnityEngine;

namespace LudumDare.Assets.Scripts.Level {
    class EnemySpawner : MonoBehaviour {
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
            Spawn();
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
            int enemyIndex = Random.Range(0, enemyPrefabs.Length - 1);
            var spawnPos = transform.position;
            if (spawnPositions.Length > 0) {
                int positionIndex = Random.Range(0, spawnPositions.Length - 1);
                spawnPos = spawnPositions[positionIndex];
            }

            var enemy = Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity);
            spawnedEnemies++;
            timer.SetForFrames(spawnDelayInFrames.RandomInRangeInclusive());
            if(enemy.TryGetComponent(out CharacterHealth health)){
                health.onDeath.AddListener(CountKilledEnemies);
            }
        }

        void CountKilledEnemies() {
            killedEnemies++;
            if(killedEnemies == totalNumberOfEnemies) {
                Debug.Log("Level Complete");
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
