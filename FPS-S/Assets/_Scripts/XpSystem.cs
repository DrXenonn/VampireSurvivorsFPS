using UnityEngine;
using UnityEngine.AI;

public class XpSystem : MonoBehaviour
{
    [HideInInspector] public int currentXP;
    [HideInInspector] public int currentLevel;
    public int xpToNextLevel;
    [SerializeField] private float xpMultiplier;
    [SerializeField] private UpgradePanelManager upgradePanelManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private GameEvent updateUI;
    [HideInInspector] public float xpValueMultiplier;
    [SerializeField] private AudioSource levelupSoundEffect;
    [SerializeField] private float pushDistance = 5f;
    [SerializeField] private AudioSource xpPickupSound;
    [SerializeField] private float minPitch, maxPitch;

    private void Start()
    {
        xpValueMultiplier = 1f;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            EarnXP(1000);
        }
    }

    private void CheckLevelUp()
    {
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        PushEnemiesAway();
        levelupSoundEffect.Play();
        upgradeManager.ShowUpgrades();

        currentXP -= xpToNextLevel;
        currentLevel++;
        updateUI.Raise();

        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpMultiplier);
        upgradePanelManager.OpenPanel(upgradeManager.shownUpgrades);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("XP"))
        {
            EarnXP((int)(other.GetComponent<XpOrb>().xpValue * xpValueMultiplier));
        }
    }

    private void EarnXP(int xpAmount)
    {
        currentXP += xpAmount;
        CheckLevelUp();
        updateUI.Raise();
        PlaySound();
    }

    private void PlaySound()
    {
        var progress = (float)currentXP / xpToNextLevel;
        var pitchRange = maxPitch - minPitch;
        var pitch = minPitch + progress * pitchRange;
        xpPickupSound.pitch = pitch;
        xpPickupSound.PlayOneShot(xpPickupSound.clip);
    }


    private void PushEnemiesAway()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pushDistance);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Vector3 pushDirection = hitCollider.transform.position - transform.position;
                pushDirection.y = 0f;
                NavMeshAgent navMeshAgent = hitCollider.GetComponent<NavMeshAgent>();
                if (navMeshAgent != null)
                {
                    Vector3 targetPosition = hitCollider.transform.position + pushDirection.normalized * pushDistance;
                    navMeshAgent.Warp(targetPosition);
                }
            }
        }
    }
}