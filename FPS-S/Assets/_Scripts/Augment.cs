using System;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Augment : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    private bool isHolding;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject defaultText;
    [SerializeField] private string augmentName;
    [SerializeField] private int maxPoints;
    [SerializeField] private int price;
    [SerializeField] private AugmentsMenu augmentManager;
    [SerializeField] private GameObject maxStacks;
    private int currentPoints;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource noCreditSound;

    private void OnEnable()
    {
        currentPoints = Math.Min(currentPoints, maxPoints);
        UpdateUI();
        OnMaxStacks();
    }

    public void OnHover()
    {
        description.SetActive(true);
        defaultText.SetActive(false);
    }

    public void OnExit()
    {
        description.SetActive(false);
        defaultText.SetActive(true);
    }

    public void OnPointerDown()
    {
        OnNoCredits();
        progressBar.fillAmount = 0.05f;
        isHolding = true;
    }

    public void OnPointerUp()
    {
        progressBar.fillAmount = 0;
        isHolding = false;
    }

    private void Update()
    {
        if (isHolding)
        {
            FillProgressBar();
            LevelUp();
        }
    }

    private void FillProgressBar()
    {
        progressBar.fillAmount += 1 * Time.deltaTime;
    }

    private bool CheckForFill()
    {
        return progressBar.fillAmount >= 0.99f;
    }

    private bool CheckCredits()
    {
        return PlayerPrefs.GetInt("Crystals") >= price;
    }

    private bool CheckMaxLevel()
    {
        return currentPoints >= maxPoints;
    }

    private void LevelUp()
    {
        if (CheckForFill() && CheckCredits())
        {
            PlayerPrefs.SetInt(augmentName, currentPoints + 1);
            progressBar.fillAmount = 0;
            UpdateUI();
            SpendCredits(price);
        }
        OnNoCredits();
        OnMaxStacks();
    }

    private void UpdateUI()
    {
        currentPoints = PlayerPrefs.GetInt(augmentName);
        tmp.text = $"{currentPoints}/{maxPoints}";
    }

    private void SpendCredits(int cost)
    {
        var currentCrystals = PlayerPrefs.GetInt("Crystals");
        PlayerPrefs.SetInt("Crystals", currentCrystals - cost);
        augmentManager.SetCrystalsUI();
    }

    private void OnMaxStacks()
    {
        if (!CheckMaxLevel()) return;
        
        isHolding = false;
        maxStacks.SetActive(true);
        DisableRaycastTarget(transform);
    }

    private void OnNoCredits()
    {
        if (!CheckCredits())
        {
            isHolding = false;
            animator.SetTrigger("NoCredit");
            noCreditSound.Play();
        }
    }

    private void DisableRaycastTarget(Transform parentTransform)
    {
        Image[] images = parentTransform.GetComponentsInChildren<Image>();

        foreach (Image image in images)
        {
            image.raycastTarget = false;
        }
    }
}