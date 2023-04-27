using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[DefaultExecutionOrder(2)]
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
        OnUIChange(null, null);
    }

    private void Update()
    {
        FillBar(manaBar, mana.currentMana, mana.maxMana);
        manaText.text = $"{mana.currentMana} | {mana.maxMana}";

        UpdateTimer();
    }

    void UpdateHealthBarColor()
    {
        float percent = ((float)health.currentHealth / (float)health.maxHealth) * 100f;
        if (percent >= 66.666f)
        {
            healthBar.color = healthyColor;
        }
        else if (percent >= 33.333f)
        {
            healthBar.color = halfHPColor;
        }
        else
        {
            healthBar.color = lowHPColor;
        }
    }


    private void UpdateText()
    {
        healthText.text = $"{health.currentHealth} | {health.maxHealth}";
        levelText.text = $"{xp.currentLevel}";
        killCount.text = enemyCounter.killedEnemies.ToString();
    }
    void FillBar(Image bar, float current, float max)
    {
        bar.fillAmount = current / max;
    }

    public void OnUIChange(Component sender, object data)
    {
        UpdateText();
        FillBar(healthBar, health.currentHealth, health.maxHealth);
        FillBar(xpBar, xp.currentXP, xp.xpToNextLevel);
        UpdateHealthBarColor();
    }

    private void UpdateTimer()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeManager.GetTimeToWin());
        timerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
