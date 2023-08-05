using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Death : MonoBehaviour
{
    public static Death Instance { get; private set; }

    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] objectsToDisable;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject stats;
    [SerializeField] private TextMeshProUGUI timeText, killsText, totalCrystalText;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private EnemyCounter enemyCounter;
    [HideInInspector] public bool isPlayerDead;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnDeath(null, null);
        }
    }

    public void OnDeath(Component component, object data)
    {
        isPlayerDead = true;
        foreach (var gObject in objectsToDisable)
        {
            gObject.SetActive(false);
        }

        animator.Play("Fade");
        gameOver.SetActive(true);
        StartCoroutine(nameof(SlowDownGameSpeed));
        pauseManager.PauseGame(false);
    }

    private IEnumerator SlowDownGameSpeed()
    {
        for (var i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.07f);

            if (Time.timeScale >= 0.1f)
            {
                Time.timeScale -= 0.1f;
            }
        }

        Time.timeScale = 0;
        MapRecords();
        stats.SetActive(true);
    }

    private void MapRecords()
    {
        var timeSpan = TimeSpan.FromSeconds(timeManager.GetTimeElapsed());
        timeText.text = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";

        killsText.text = enemyCounter.killedEnemies.ToString();

        var crystals = Mathf.FloorToInt(enemyCounter.killedEnemies + timeManager.GetTimeElapsed());
        totalCrystalText.text = crystals.ToString();
        
        var currentCrystals = PlayerPrefs.GetInt("Crystals");
        PlayerPrefs.SetInt("Crystals", currentCrystals + crystals);
    }

    public void OnContinue()
    {
        SceneManager.Instance.LoadScene("Main Menu");
    }
}