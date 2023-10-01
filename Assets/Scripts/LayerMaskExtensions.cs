using UnityEngine;

namespace LudumDare {
    public static class LayerMaskExtensions {
        public static bool IsInLayerMask(this LayerMask layerMask, GameObject obj) {
            int objLayer = obj.layer;
            return (layerMask.value & (1 << objLayer)) > 0;
        }
    }
}