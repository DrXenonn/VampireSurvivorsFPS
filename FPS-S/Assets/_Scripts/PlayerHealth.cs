using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [HideInInspector] public float currentHealth;

    private void Start()
    {
        currentHealth = playerStats.maxHealth;    
    }

    private void Update()
    {
        Debug.Log(currentHealth + " / " + playerStats.maxHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
