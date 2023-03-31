using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    private void Start()
    {
        Physics.IgnoreLayerCollision(9, 3);
    }
}
