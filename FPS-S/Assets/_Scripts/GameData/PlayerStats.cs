using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; set; }

    public int maxHealth;
    public float movementSpeed;
    public float dashDistance;
    public float projectileSpeed;
    public float fireRate;
    public uint maxMana;
    public int primaryDamage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
