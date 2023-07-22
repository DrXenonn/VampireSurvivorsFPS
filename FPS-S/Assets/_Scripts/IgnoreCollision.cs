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
            bool ignoreCollision = Physics.GetIgnoreLayerCollision(layerPairsToIgnore[i].layerA, layerPairsToIgnore[i].layerB);

            Physics.IgnoreLayerCollision(layerPairsToIgnore[i].layerA, layerPairsToIgnore[i].layerB, !ignoreCollision);
            Physics.IgnoreLayerCollision(layerPairsToIgnore[i].layerB, layerPairsToIgnore[i].layerA, !ignoreCollision);
        }
    }
}