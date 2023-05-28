using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [System.Serializable]
    private struct LayerPair
    {
        public int layerA;
        public int layerB;
    }

    [SerializeField] private LayerPair[] layerPairsToIgnore;

    private void OnValidate()
    {
        SetCollisionMatrix();
    }

    private void SetCollisionMatrix()
    {
        var numPairs = layerPairsToIgnore.Length;

        for (var i = 0; i < numPairs; i++)
        {
            var collision = Physics.GetIgnoreLayerCollision(layerPairsToIgnore[i].layerA, layerPairsToIgnore[i].layerB) ? 0 : 1;

            Physics.IgnoreLayerCollision(layerPairsToIgnore[i].layerA, layerPairsToIgnore[i].layerB, collision == 0);
            Physics.IgnoreLayerCollision(layerPairsToIgnore[i].layerB, layerPairsToIgnore[i].layerA, collision == 0);
        }
    }
}
