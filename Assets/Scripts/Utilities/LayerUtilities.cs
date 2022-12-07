using UnityEngine;

public static class LayerUtilities
{
    private static bool ContainsLayer(LayerMask layerMask, int intLayer) => (1 << intLayer & layerMask) != 0;
    public static bool IsSameLayer(LayerMask layerMask, int intLayer) => ContainsLayer(layerMask, intLayer);
}
