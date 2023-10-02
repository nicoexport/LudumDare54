using System.Collections.Generic;
using System.Linq;
using LudumDare.Assets.Scripts;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace LudumDare {
    public class AreaGrid : MonoBehaviour {
        [SerializeField]
        Area areaPrefab;

        [SerializeField]
        int m_gridWidth = 10;

        [SerializeField]
        int m_gridMaxHeight = 5;

        int gridWidth => m_gridWidth + 2;
        int gridMaxHeight => m_gridMaxHeight + 2;


        [SerializeField]
        bool startBig = true;

        Dictionary<Vector2, Area> positionAreaPairs = new();
        Dictionary<Vector2, Area> positionDeathAreaPairs = new();

        List<int> healthSegments = new();
        int lastCurrentHealth;
        System.Random random = new System.Random();
        bool isHealthSegmentGenerated = false;

        protected void OnEnable() {
            Debug.Log(nameof(OnEnable));
            Player.onHealthChanged += OnPlayerHealthChanged;
        }

        protected void OnDisable() {
            Player.onHealthChanged -= OnPlayerHealthChanged;
            Debug.Log(nameof(OnDisable));   
        }
    
        [ContextMenu("Generate")]
        protected void Start() {
            Debug.Log("Start");
            
        }

        protected void Awake() {
            Debug.Log("Awake");
            ClearGrid();
            GenerateMap();
            SetGridPos();
            GenerateHealthSegments(GameConfigManager.Instance.gameConfig.playerMaxHealth);
        }

        void SetGridPos() {
            float x = (float)((gridWidth - 1) * areaPrefab.GetWidth()) / 2f;
            float y = (gridMaxHeight - 2) * areaPrefab.GetHeight() / 2f;
            transform.position -= new Vector3(x, y);
        }

        void GenerateMap() {
            for (int x = 0; x < gridWidth; x++) {

                // max-grid height 1 should generate a straigt line of tiles
                int height = 0;
                if (gridMaxHeight == 1) {
                    height = 1;
                }

                // generates pattern small-big-small
                if (startBig && gridMaxHeight > 1) {
                    height = (x % 2) == 0 ? gridMaxHeight - 1 : gridMaxHeight;
                }

                // generates pattern big-small-big
                if (!startBig && gridMaxHeight > 1) {
                    height = (x % 2) == 1 ? gridMaxHeight - 1 : gridMaxHeight;
                }


                for (int y = 0; y < height; y++) {

                    float xPos = x * areaPrefab.GetWidth();
                    float yPos = 0;

                    if (gridMaxHeight == 1) {
                        yPos = y * areaPrefab.GetHeight();
                    } else if (startBig && gridMaxHeight > 1) {
                        yPos = (x % 2) == 1 ? (y * areaPrefab.GetHeight()) - CalcOffset() : y * areaPrefab.GetHeight();
                    } else {
                        yPos = (x % 2) == 1 ? (y * areaPrefab.GetHeight()) + CalcOffset() : y * areaPrefab.GetHeight();
                    }


                    var spawnedArea = Instantiate(areaPrefab, new Vector2(xPos, yPos), Quaternion.identity, transform);

                    if (x == 0 || x == gridWidth - 1 || y == 0 || y == height - 1) {
                        spawnedArea.SetIsWalkable(false);
                        positionDeathAreaPairs.Add(spawnedArea.transform.position, spawnedArea);
                    } else {
                        positionAreaPairs.Add(spawnedArea.transform.position, spawnedArea);
                    }
                }
            }
        }

        float CalcOffset() {
            return (float)areaPrefab.GetHeight() / 2;
        }

        void OnPlayerHealthChanged(int maxHealth, int currentHealth) {
            Debug.Log("OnPlayerHealthChanged");
            if (!isHealthSegmentGenerated) {
                lastCurrentHealth = maxHealth;
                Debug.Log("Segments not generated");
                return;
            }

            int lastCurrentHealthUpperSegmentThreshold = GetCurrentUpperSegmentThreshold(lastCurrentHealth);
            int currentHealthLowerSegmentThreshold = GetLowerSegmentThreshold(currentHealth);
            int platformAmount = Mathf.Abs((lastCurrentHealthUpperSegmentThreshold - currentHealthLowerSegmentThreshold) / (maxHealth / positionAreaPairs.Count));

            if (currentHealth > lastCurrentHealth) {
                int counter = 0;
                while (counter < platformAmount) {
                    EnableRandomWalkableArea();
                    counter++;
                }
            }

            if (currentHealth < lastCurrentHealth) {
                int counter = 0;
                while (counter < platformAmount) {
                    DisableRandomWalkableArea();
                    counter++;
                }
            }
            lastCurrentHealth = currentHealth;
        }

        void GenerateHealthSegments(int maxHealth) {
            // calc threshhold
            int treshhold = maxHealth / positionAreaPairs.Count;

            // calc threshhold points
            int healthSegment = maxHealth;
            healthSegments.Add(healthSegment);
            while (healthSegment > 0) {
                healthSegments.Add(healthSegment -= treshhold);
            }

            isHealthSegmentGenerated = true;
            lastCurrentHealth = maxHealth;
        }

        void DisableRandomWalkableArea() {
            // grab random area
            var matching = positionAreaPairs.Where(x => x.Value.GetIsWalkable()).ToDictionary(x => x.Key, x => x.Value);
            var element = positionAreaPairs.ElementAt(random.Next(0, matching.Count)).Value;
            // make it unwalkable
            element.SetIsWalkable(false);
        }

        void EnableRandomWalkableArea() {
            // grab random area
            var matching = positionAreaPairs.Where(x => !x.Value.GetIsWalkable()).ToDictionary(x => x.Key, x => x.Value);
            var element = matching.ElementAt(random.Next(0, matching.Count)).Value;

            // make it walkable
            element.SetIsWalkable(true);
        }

        int GetCurrentUpperSegmentThreshold(int health) {

            if (health >= healthSegments[0]) {
                return healthSegments[0];
            }

            foreach (int segment in healthSegments) {
                if (health > segment) {

                    return healthSegments[healthSegments.IndexOf(segment) - 1];
                }
            }
            return -1;
        }

        int GetLowerSegmentThreshold(int health) {

            if (health <= healthSegments[^1]) {
                return healthSegments[^1];
            }

            foreach (int segment in healthSegments) {
                if (health > segment) {
                    return segment;
                }
            }
            return -1;
        }

        void ClearGrid() {
            positionAreaPairs.Clear();

            foreach (var child in transform.GetChildren()) {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}