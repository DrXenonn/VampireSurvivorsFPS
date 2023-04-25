using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    private Mana mana;
    private PlayerHealth health;
    private XpSystem xp;
    private TimeManager timeManager;
    private EnemyCounter enemyCounter;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image xpBar;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCount;

    [SerializeField] private Color healthyColor;
    [SerializeField] private Color halfHPColor;
    [SerializeField] private Color lowHPColor;


    private void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        health = FindObjectOfType<PlayerHealth>();
        mana = FindObjectOfType<Mana>();
        xp = FindObjectOfType<XpSystem>();
        enemyCounter = FindObjectOfType<EnemyCounter>();
    }

    private void Update()
    {
        UpdateText();
        UpdateHealthBarColor();

        FillBar(manaBar, mana.currentMana, mana.maxMana);
        FillBar(healthBar, health.currentHealth, health.maxHealth);
        FillBar(xpBar, xp.currentXP, xp.xpToNextLevel);
    }

    void UpdateHealthBarColor()
    {
        if (health.currentHealth / health.maxHealth * 100 < 35)
        {
            healthBar.color = lowHPColor;
        }
        else if (health.currentHealth / health.maxHealth * 100 < 68)
        {
            healthBar.color = halfHPColor;
        }
        else
        {
            healthBar.color = healthyColor;
        }
    }
    private void UpdateText()
    {
        healthText.text = $"{health.currentHealth} | {health.maxHealth}";
        manaText.text = $"{mana.currentMana} | {mana.maxMana}";
        levelText.text = $"{xp.currentLevel}";

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeManager.GetTimeToWin());
        timerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        killCount.text = enemyCounter.killedEnemies.ToString();
    }
    void FillBar(Image bar, float current, float max)
    {
        bar.fillAmount = current / max;
    }

    public void OnUIChange() 
    {

    }
}
