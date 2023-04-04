using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float maxHealth;


    private void Start()
    {
        currentHealth = playerStats.maxHealth;    
        maxHealth = playerStats.maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
