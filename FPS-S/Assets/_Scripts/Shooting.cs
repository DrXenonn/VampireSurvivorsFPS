using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Shooting : MonoBehaviour
{
    public static Shooting Instance { get; private set; }

    #region variables

    [Header("References")] [SerializeField]
    public Transform firePos;

    [SerializeField] protected Mana manaManager;
    [SerializeField] protected Animator staffAnimator;
    [SerializeField] protected Crosshair crosshair;
    [SerializeField] protected AudioSource shootingSound;

    [Header("Variables")] [SerializeField] protected int secondaryFireProjectileCount;
    [SerializeField] protected float secondaryFireRate;
    public bool arc;
    [SerializeField] protected float arcRange;
    [SerializeField] protected float arcTime;
    [SerializeField] protected float minimumDistanceToArc;
    [SerializeField] protected int primaryFireManaCost;
    [SerializeField] protected int secondaryFireManaCost;
    [SerializeField] protected GameObject projectilePrefab;
    private static readonly int Shoot = Animator.StringToHash("Shoot");

    public enum SecondaryMode
    {
        None,
        Burst,
        AllIn
    }

    public SecondaryMode secondaryMode;

    private float nextFireTime;
    private ObjectPool<GameObject> pool;

    [Header("Camera Shake")] [SerializeField]
    protected float amplitude;

    [SerializeField] protected float frequency;
    [SerializeField] protected float duration;

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

    private void ShootSecondaryFire()
    {
        switch (secondaryMode)
        {
            case SecondaryMode.None:
                return;
            case SecondaryMode.Burst:
                ShootBurst();
                break;
            case SecondaryMode.AllIn:
                AllIn();
                break;
        }
    }

    private void ShootPrimaryFire()
    {
        if (Time.time >= nextFireTime && manaManager.currentMana >= primaryFireManaCost)
        {
            shootingSound.pitch = Random.Range(1.1f, 1.7f);
            shootingSound.PlayOneShot(shootingSound.clip);
            manaManager.currentMana -= primaryFireManaCost;
            nextFireTime = Time.time + 1 / PlayerStats.Instance.fireRate;
            staffAnimator.SetTrigger(Shoot);
            pool.Get();
        }
    }

    private void ShootBurst()
    {
        if (Time.time >= nextFireTime && manaManager.currentMana >= secondaryFireManaCost)
        {
            manaManager.currentMana -= secondaryFireManaCost;
            staffAnimator.SetTrigger(Shoot);
            StartCoroutine(SpawnProjectiles(secondaryFireProjectileCount, secondaryFireRate));
        }
    }

    private void AllIn()
    {
        // ReSharper disable once PossibleLossOfFraction
        if (manaManager.currentMana >= primaryFireManaCost)
        {
            var numProjectiles = Mathf.FloorToInt(manaManager.currentMana / primaryFireManaCost);
            manaManager.currentMana = 0;
            staffAnimator.SetTrigger(Shoot);
            StartCoroutine(SpawnProjectiles(numProjectiles, 0.05f));
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
        projectile.SetActive(true);

        ResetProjectile(projectile);
        AddArcEffect(projectile);
        CameraShaker.ShakeCamera(amplitude, frequency, duration);
        StartCoroutine(ReleaseProjectileDelayed(projectile, 2f));
    }


    private void AddArcEffect(GameObject gameObject)
    {
        if (!arc) return;

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
        projectile.transform.position = firePos.position;
        iTween.Stop(projectile);
    }


    private IEnumerator SpawnProjectiles(int num, float time)
    {
        for (var i = 0; i < num; i++)
        {
            shootingSound.pitch = Random.Range(1.2f, 1.75f);
            shootingSound.PlayOneShot(shootingSound.clip);
            pool.Get();
            nextFireTime = Time.time + 1 / (PlayerStats.Instance.fireRate / 2);
            yield return new WaitForSeconds(time);
        }
    }
}