using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class XpPool : MonoBehaviour
{
    public static ObjectPool<GameObject> xpPool;
    private GameObject xpPrefab;
    private Transform parent;

    private void Start()
    {
        xpPool = new ObjectPool<GameObject>(
            createFunc: () => CreateXP(),
            actionOnGet: (obj) => OnGetFromPool(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 60,
            maxSize: 500);

    }

    public void GetMessage(Component component, object obj)
    {
        xpPrefab = (GameObject)obj;
        parent = component.transform;
        xpPool.Get();
        Debug.Log($"InActive: {xpPool.CountInactive} Active: {xpPool.CountActive}");
    }

    private GameObject CreateXP()
    {
        var obj = Instantiate(xpPrefab, parent.position, Quaternion.identity);
        return obj;
    }

    private void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
        obj.transform.position = parent.position;
    }


    public static void ReleaseXP(Component sender, object data)
    {
        xpPool.Release(sender.gameObject);
    }
}
