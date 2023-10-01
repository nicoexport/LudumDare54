using System;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare {

    public class AreaGrid : MonoBehaviour {
        [SerializeField]
        Area areaPrefab;

        [SerializeField]
        int gridWidth = 10;

        [SerializeField]
        int gridMaxHeight = 5;

        List<Area> areas = new List<Area>();


        protected void Start() {
            areas.Clear();
            GenerateMap();
        }

        void GenerateMap() {
            for (int x = 0; x < gridWidth; x++) {
                int height = (x % 2) == 0 ? gridMaxHeight - 1 : gridMaxHeight;
                for (int y = 0; y < height; y++) {

                    float xPos = x * areaPrefab.GetWidth();
                    float yPos = (x % 2) == 1 ? (y * areaPrefab.GetHeight()) - CalcOffset() : y * areaPrefab.GetHeight();

                    var spawnedArea = Instantiate(areaPrefab, new Vector3(xPos, yPos), Quaternion.identity, transform);
                    Debug.Log($"{xPos}, {yPos}");
                    areas.Add(spawnedArea);
                }
            }
        }

        public List<Area> GetNeighbors() {
            return null;
        }

        float CalcOffset() {
            return areaPrefab.GetHeight() / 2;
        }
    }
}