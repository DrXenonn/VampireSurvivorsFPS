using Unity.Mathematics;
using UnityEngine;

public class EnemyVFXManager : MonoBehaviour
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private float yOffset;
    [SerializeField] private float vfxTimeToLive;

    public void OnEnemyDeathOrSpawn(Component sender, object data)
    {
        var position = sender.transform.position;
        var newPosition = new Vector3(position.x, position.y + yOffset, position.z);
        var obj = Instantiate(vfx, newPosition, quaternion.identity);
        Destroy(obj,vfxTimeToLive);
    }
}