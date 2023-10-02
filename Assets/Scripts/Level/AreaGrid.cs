using System.Collections.Generic;
using System.Linq;
using LudumDare.Assets.Scripts;
using MyBox;
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

        System.Random random = new();
        bool isHealthSegmentGenerated = false;

        [SerializeField, ReadOnly]
        List<MinMaxFloat> segments = new();
        int currentSegmentIndex = int.MaxValue;

        protected void OnEnable() {
            Player.onHealthChanged += OnPlayerHealthChanged;
        }

        protected void OnDisable() {
            Player.onHealthChanged -= OnPlayerHealthChanged;
        }

        protected void Awake() {
            Debug.Log("Awake");
            ClearGrid();
            GenerateMap();
            SetGridPos();
            GenerateHealthSegments(GameConfigManager.Instance.gameConfig.playerMaxHealth);
        }

        void SetGridPos() {
            float x = (gridWidth - 1) * areaPrefab.GetWidth() / 2f;
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
            if (!isHealthSegmentGenerated) {
                return;
            }

            int newIndex = GetCurrentSegmentIndex(currentHealth);
            int amount = currentSegmentIndex - newIndex;
            

            if (amount < 0) {
                for (int i = 0; i < Mathf.Abs(amount); i++) {
                    EnableRandomWalkableArea();
                }
            }

            if (amount > 0) {
                for (int i = 0; i < Mathf.Abs(amount); i++) {
                    DisableRandomWalkableArea();
                }
            }

            if(currentHealth <= 0) {
                DisableRemaining();
            }

            currentSegmentIndex = newIndex;
        }

        void DisableRemaining() {
            var values = positionAreaPairs.Values.ToList();
            var matching = values.Where(x => x.GetIsWalkable()).ToList().ForAll(x => x.SetIsWalkable(false));       
        }

        void GenerateHealthSegments(int maxHealth) {
            // calc threshhold
            float treshhold = (float)maxHealth / positionAreaPairs.Count;
            float currentBottom = 0;
            for (int i = 0; i < positionAreaPairs.Count; i++, currentBottom += treshhold) {
                float currentTop = currentBottom + treshhold;
                var segment = new MinMaxFloat(currentBottom, currentTop);
                segments.Add(segment);
            }
            currentSegmentIndex = segments.Count - 1;
            isHealthSegmentGenerated = true;
        }

        void DisableRandomWalkableArea() {
            // grab random area
            var values = positionAreaPairs.Values.ToList();
            var matching = values.Where(x => x.GetIsWalkable() && !x.hasPlayer).ToList();
            var element = matching[Random.Range(0, matching.Count)];
            // make it unwalkable
            element.SetIsWalkable(false);
        }

        void EnableRandomWalkableArea() {
            // grab random area
            var values = positionAreaPairs.Values.ToList();
            var matching = values.Where(x => !x.GetIsWalkable()).ToList();
            var element = matching[Random.Range(0, matching.Count)];
            // make it walkable
            element.SetIsWalkable(true);
        }

        int GetCurrentSegmentIndex(int value) {
            if(value > GameConfigManager.Instance.gameConfig.playerMaxHealth) {
                return segments.Count - 1;
            }

            if(value < 0) {
                return 0;   
            }

            var item = segments.Where(x => x.Min <= value && value <= x.Max);

            return segments.IndexOfItem(item.ToArray()[0]);
        }

        void ClearGrid() {
            positionAreaPairs.Clear();

            foreach (var child in transform.GetChildren()) {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}