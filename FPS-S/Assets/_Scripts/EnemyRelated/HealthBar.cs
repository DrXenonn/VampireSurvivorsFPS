using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private float reduceSpeed = 4.5f;
    private float target = 1f;

    public void UpdateHealthbar(float maxHealth, float currnetHealth)
    {
        target = currnetHealth / maxHealth;
    }

    private void Update()
    {
        healthbarSprite.fillAmount = Mathf.MoveTowards(healthbarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }

}
