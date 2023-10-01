using System.Collections.Generic;
using System.Linq;
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

        protected void OnEnable() {
            // subscribe on player health treshhold event
        }

        protected void OnDisable() {
            // unsubscribe on player health treshhold event
        }

        [ContextMenu("Generate")]
        protected void Start() {
            ClearGrid();
            GenerateMap();
            SetGridPos();
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

                    if(x == 0 || x == gridWidth - 1 || y == 0 ||  y == height -1) {
                        spawnedArea.SetIsWalkable(false);
                    }
                }
            }
        }

        float CalcOffset() {
            return (float)areaPrefab.GetHeight() / 2;
        }

        void DisableWalkableArea() {
            // grab random area
            var rand = new System.Random();
            var element = positionAreaPairs.ElementAt(rand.Next(0, positionAreaPairs.Count)).Value;
            // make it unwalkable
            element.SetIsWalkable(false);
        }

        void ClearGrid() {
            positionAreaPairs.Clear();

            foreach (var child in transform.GetChildren()) {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}