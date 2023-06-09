using UnityEngine;
using UnityEngine.Pool;

public class XpPool : MonoBehaviour
{
    private ObjectPool<GameObject> xpPool;
    private GameObject xpPrefab;
    private Transform parent;
    [HideInInspector] public float twinOrbsChance = 0;

    private void Start()
    {
        xpPool = new ObjectPool<GameObject>(
            createFunc: () => CreateXP(),
            actionOnGet: (obj) => OnGetFromPool(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 70,
            maxSize: 300);

    }

    public void GetMessage(Component component, object obj)
    {
        xpPrefab = (GameObject)obj;
        parent = component.transform;
        xpPool.Get();

        if (Random.value < twinOrbsChance)
        {
            xpPool.Get();
        }
    }

    private GameObject CreateXP()
    {
        var obj = Instantiate(xpPrefab, parent.position, Quaternion.identity);
        return obj;
    }

    private void OnGetFromPool(GameObject obj)
    {
        obj.transform.position = parent.position;
        obj.SetActive(true);
    }


    public void ReleaseXP(Component sender, object data)
    {
        xpPool.Release(sender.gameObject);
    }
}
