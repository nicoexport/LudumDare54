using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;


namespace LudumDare.Assets.Scripts.Character {
    class SpriteFlasher : ComponentFeature<SpriteRenderer> {
        [SerializeField] Color flashColor;
        [SerializeField] float flashDurationInSeconds = 0.2f;
        [SerializeField] Ease ease;
        [SerializeField] bool trigger;
        Color defaultColor;
        Tween tween;

        protected void Awake() {
            defaultColor = attachedComponent.color;
        }

        protected void Update() {
            if (trigger) {
                trigger = !trigger;
                Flash();
            }
        }

        [ContextMenu("Flash")]
        public void Flash() {
            Cancel();
            tween = attachedComponent.DOColor(flashColor, flashDurationInSeconds).SetEase(ease).From();
        }

        public void Cancel() {
            if(tween == null || !tween.IsActive()) {
                return;
            }
            tween.Kill();
            attachedComponent.color  = defaultColor;
        }
    }
}
