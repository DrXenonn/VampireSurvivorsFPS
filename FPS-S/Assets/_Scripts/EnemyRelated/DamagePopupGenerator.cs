using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class DamagePopupGenerator : MonoBehaviour
{
    public static DamagePopupGenerator instance;
    [SerializeField] private GameObject damagePopupPrefab;
    [SerializeField] private float randomness;
    [SerializeField] private float yOffset;

    [SerializeField] private Color lowDamageColor, mediumDamageColor, highDamageColor, criticalDamageColor;

    public ObjectPool<GameObject> pool;
    private Vector3 posToSpawn;
    private int damageToShow;
    
    private void Awake()
    {
        instance = this;
        pool = new ObjectPool<GameObject>(
            createFunc: CreatePopup,
            actionOnGet: OnGetFromPool,
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: Destroy,
            collectionCheck: false,
            defaultCapacity: 50,
            maxSize: 100);
    }
    
    
    public void RequestDamagePopup(Vector3 position, int damage)
    {
        posToSpawn = position;
        damageToShow = damage;
        pool.Get();
    }

    private GameObject CreatePopup()
    {
        var popup = Instantiate(damagePopupPrefab, posToSpawn + GetRandomPosition(), Quaternion.identity);
        InitTextMeshPro(popup);

        return popup;
    }

    private void OnGetFromPool(GameObject obj)
    {
        obj.transform.position = posToSpawn + GetRandomPosition();
        InitTextMeshPro(obj);
        
        obj.SetActive(true);
    }

    private void InitTextMeshPro(GameObject obj)
    {
        var tmp = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        CalculateFontSize(posToSpawn, tmp);
        SetColor(tmp);
        tmp.text = damageToShow.ToString();
    }

    private void SetColor(TextMeshProUGUI tmp)
    {
        var damage = PlayerStats.Instance.primaryDamage;
        
        if (damageToShow > damage * 2)
        {
            tmp.faceColor = criticalDamageColor;
        }
        else if (damageToShow > damage)
        {
            tmp.faceColor = highDamageColor;
        }
        else if(damageToShow == damage)
        {
            tmp.faceColor = mediumDamageColor;
        }
        else
        {
            tmp.faceColor = lowDamageColor;
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(GetRandomNumber(), (GetRandomNumber() / 4) + yOffset, GetRandomNumber());
    }
    private float GetRandomNumber()
    {
        return Random.Range(-randomness, randomness);
    }
    private void CalculateFontSize(Vector3 pos, TextMeshProUGUI tmp)
    {
        var distance = Vector3.Distance(Camera.main!.transform.position, pos);
        
        if (distance > 70)
        {
            tmp.fontSize = 36f;
        }
        else if (distance > 50)
        {
            tmp.fontSize = 24f;
        }
        else if (distance > 35)
        {
            tmp.fontSize = 18f;
        }
        else if (distance > 20)
        {
            tmp.fontSize = 15f;
        }
    }
}