using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Shooting : MonoBehaviour
{
    public static Shooting Instance { get; private set; }

    #region variables
    [Header("Refrences")]
    [SerializeField] public Transform firePos;
    [SerializeField] private Mana manaManager;
    [SerializeField] private Animator staffAnimator;
    [SerializeField] private Crosshair crosshair;
    [SerializeField] private AudioSource shootingSound;

    [Header("Variables")]
    [SerializeField] private float secondaryFireProjectileCount;
    [SerializeField] private float secondaryFireRate;
    [SerializeField] private float arcRange;
    [SerializeField] private float arcTime;
    [SerializeField] private float minimumDistanceToArc;
    [SerializeField] private int primaryFireManaCost;
    [SerializeField] private int secondaryFireManaCost;
    [SerializeField] private GameObject projectilePrefab;
    private float nextFireTime;
    private ObjectPool<GameObject> pool;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => InstantiateProjectile(),
            actionOnGet: (obj) => OnGetProjectile(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 50,
            maxSize: 200);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ShootPrimaryFire();
        }

        if (Input.GetMouseButton(1))
        {
            ShootSecondaryFire();
        }
    }

    private void ShootPrimaryFire()
    {
        if (Time.time >= nextFireTime && manaManager.currentMana >= primaryFireManaCost)
        {
            shootingSound.pitch = Random.Range(0.9f, 1.35f);
            shootingSound.PlayOneShot(shootingSound.clip);
            manaManager.currentMana -= primaryFireManaCost;
            nextFireTime = Time.time + 1 / PlayerStats.Instance.fireRate;
            staffAnimator.SetTrigger("Shoot");
            pool.Get();
        }
    }

    private void ShootSecondaryFire()
    {
        if (Time.time >= nextFireTime && manaManager.currentMana >= secondaryFireManaCost)
        {
            manaManager.currentMana -= secondaryFireManaCost;
            staffAnimator.SetTrigger("Shoot");
            StartCoroutine(SpawnProjectiles(secondaryFireRate));
        }
    }

    private GameObject InstantiateProjectile()
    {
        var projectile = Instantiate(projectilePrefab, firePos.position, Quaternion.identity) as GameObject;

        AddArcEffect(projectile);
        return projectile;
    }

    private IEnumerator ReleaseProjectileDelayed(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        pool.Release(projectile);
    }

    private void OnGetProjectile(GameObject projectile)
    {
        StartCoroutine(ActivateProjectileDelayed(projectile, 0.02f));

        ResetProjectile(projectile);
        AddArcEffect(projectile);
        StartCoroutine(ReleaseProjectileDelayed(projectile, 2f));
    }


    private void AddArcEffect(GameObject gameObject)
    {
        if (crosshair.hit.distance > minimumDistanceToArc || crosshair.hit.point == Vector3.zero)
        {
            iTween.PunchPosition(gameObject, new Vector3(
                Random.Range(-arcRange - 3f, arcRange + 3f),
                Random.Range(0.25f, arcRange + 0.25f), 0),
                arcTime);
        }
    }

    private void ResetProjectile(GameObject projectile)
    {
        iTween.PunchPosition(gameObject, Vector3.zero,0);
        projectile.transform.position = firePos.position;
    }

    private IEnumerator ActivateProjectileDelayed(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        projectile.SetActive(true);
    }

    private IEnumerator SpawnProjectiles(float time)
    {
        for (int i = 0; i < secondaryFireProjectileCount; i++)
        {
            shootingSound.pitch = Random.Range(0.9f, 1.35f);
            shootingSound.PlayOneShot(shootingSound.clip);
            pool.Get();
            nextFireTime = Time.time + 1 / PlayerStats.Instance.fireRate;
            yield return new WaitForSeconds(time);
        }
    }
}

