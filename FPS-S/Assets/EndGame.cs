using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EndGame : MonoBehaviour
{
    public static EndGame Instance { get; private set; }

    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] objectsToDisable;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject stats;
    [SerializeField] private TextMeshProUGUI timeText, killsText, totalCrystalText;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private EnemyCounter enemyCounter;
    [HideInInspector] public bool isGameEnded;
    [SerializeField] private string loseTitle, winTitle;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnWin(null, null);
        }
    }

    public void OnDeath(Component component, object data)
    {
        FinishGame(loseTitle);
    }
    
    public void OnWin(Component component, object data)
    {
        FinishGame(winTitle);
    }

    private void FinishGame(string titleText)
    {
        isGameEnded = true;
        foreach (var gObject in objectsToDisable)
        {
            gObject.SetActive(false);
        }

        animator.Play("Fade");
        title.SetActive(true);
        title.GetComponent<TextMeshProUGUI>().text = titleText;
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