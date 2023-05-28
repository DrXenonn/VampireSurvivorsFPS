using TMPro;
using UnityEngine;


public class DamagePopupGenerator : MonoBehaviour
{
    public static DamagePopupGenerator instance;
    [SerializeField] private GameObject damagePopupPrefab;
    [SerializeField] private float timeToLive = 1f;
    [SerializeField] private float randomness;
    [SerializeField] private float yOffset;

    private void Awake()
    {
        instance = this;
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(GetRandomNumber(), (GetRandomNumber() / 4) + yOffset, GetRandomNumber());
    }

    private float GetRandomNumber()
    {
        return Random.Range(-randomness, randomness);
    }

    public void CreatePopup(Vector3 position, int damage, Color color)
    {
        var popup = Instantiate(damagePopupPrefab, position + GetRandomPosition(), Quaternion.identity);
        var tmp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        CalculateFontSize(popup, tmp);
        tmp.text = damage.ToString();
        tmp.faceColor = color;

        Destroy(popup, timeToLive);
    }

    private void CalculateFontSize(GameObject obj, TextMeshProUGUI tmp)
    {
        var distance = obj.GetComponent<Billboarding>().distanceToCamera;
        Debug.Log(distance);
    
        if (distance > 70)
        {
            tmp.fontSize *= 3f;
        }
        else if (distance > 50)
        {
            tmp.fontSize *= 2f;
        }
        else if (distance > 35)
        {
            tmp.fontSize *= 1.5f;
        }
        else if (distance > 20)
        {
            tmp.fontSize *= 1.25f;
        }
    }

}