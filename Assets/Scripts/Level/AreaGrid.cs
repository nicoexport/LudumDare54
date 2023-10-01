using System;
using System.Collections.Generic;
using System.Linq;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace LudumDare {

    public class AreaGrid : MonoBehaviour {
        [SerializeField]
        Area areaPrefab;

        [SerializeField]
        int gridWidth = 10;

        [SerializeField]
        int gridMaxHeight = 5;

        [SerializeField]
        bool startSmall = true;

        Dictionary<Vector2, Area> positionAreaPairs = new Dictionary<Vector2, Area>();



        private void OnEnable() {
            // subscribe on player health treshhold event
        }

        private void OnDisable() {
            // unsubscribe on player health treshhold event
        }

        protected void Start() {
            positionAreaPairs.Clear();
            GenerateMap();
        }

        void GenerateMap() {
            for (int x = 0; x < gridWidth; x++) {

                // max-grid height 1 should generate a straigt line of tiles
                int height = 0;
                if (gridMaxHeight == 1) {
                    height = 1;
                }

                // generates pattern small-big-small
                if (startSmall && gridMaxHeight > 1) {
                    height = (x % 2) == 0 ? gridMaxHeight - 1 : gridMaxHeight;
                }

                // generates pattern big-small-big
                if (!startSmall && gridMaxHeight > 1) {
                    height = (x % 2) == 1 ? gridMaxHeight - 1 : gridMaxHeight;
                }


                for (int y = 0; y < height; y++) {

                    float xPos = x * areaPrefab.GetWidth();
                    float yPos = 0;

                    if (gridMaxHeight == 1) {
                        yPos = y * areaPrefab.GetHeight();
                    } else if (startSmall && gridMaxHeight > 1) {
                        yPos = (x % 2) == 1 ? (y * areaPrefab.GetHeight()) - CalcOffset() : y * areaPrefab.GetHeight();
                    } else {
                        yPos = (x % 2) == 1 ? (y * areaPrefab.GetHeight()) + CalcOffset() : y * areaPrefab.GetHeight();
                    }


                    var spawnedArea = Instantiate(areaPrefab, new Vector2(xPos, yPos), Quaternion.identity, transform);
                    spawnedArea.SetOrderInLayer(-(int)Math.Floor(yPos));
                    positionAreaPairs[new Vector2(xPos, yPos)] = spawnedArea;
                }
            }
        }

        float CalcOffset() {
            return (float)areaPrefab.GetHeight() / 2;
        }

        private void DisableWalkableArea() {
            // grab random area
            var rand = new System.Random();
            var element = positionAreaPairs.ElementAt(rand.Next(0, positionAreaPairs.Count)).Value;
            // make it unwalkable
            element.SetIsWalkable(false);
        }
    }
}