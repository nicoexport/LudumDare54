﻿using UnityEngine;

namespace LudumDare.Assets.Scripts.Level {
    public class SpriteRendererOrderSetter : ComponentFeature<SpriteRenderer> {
        protected void Start() {
            attachedComponent.sortingOrder = -(int)transform.position.y;
        }

        protected override void OnValidate() {
            base.OnValidate();
            Start();
        }
    }
}
