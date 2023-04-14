using UnityEngine;

public class Shooting : MonoBehaviour
{
    #region variables
    [Header("Refrences")]
    [SerializeField] private Transform firePos;
    [SerializeField] private Mana manaManager;
    [SerializeField] private Animator staffAnimator;
    [SerializeField] private Crosshair crosshair;

    [Header("Variables")]
    [SerializeField] private float arcRange;
    [SerializeField] private float projectileCount;
    [SerializeField] private float minArcTime, maxArcTime;
    [SerializeField] private float minimumDistanceToArc;
    [SerializeField] private int primaryFireManaCost;
    [SerializeField] private int secondaryFireManaCost;
    [SerializeField] private GameObject projectilePrefab;
    private float nextFireTime;
    #endregion


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
            manaManager.currentMana -= primaryFireManaCost;
            nextFireTime = Time.time + 1 / PlayerStats.Instance.fireRate;
            staffAnimator.SetTrigger("Shoot");
            InstantiateProjectile();
        }
    }

    private void ShootSecondaryFire()
    {
        if (Time.time >= nextFireTime && manaManager.currentMana >= secondaryFireManaCost)
        {
            manaManager.currentMana -= secondaryFireManaCost;
            nextFireTime = Time.time + 1 / PlayerStats.Instance.fireRate;
            staffAnimator.SetTrigger("Shoot");
            for (int i = 0; i < projectileCount; i++)
            {
                InstantiateProjectile();
            }
        }
    }

    private void InstantiateProjectile()
    {
        var projectile = Instantiate(projectilePrefab, firePos.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody>().velocity = ((crosshair.destination - firePos.position).normalized * PlayerStats.Instance.projectileSpeed);

        if (crosshair.hit.distance < minimumDistanceToArc && crosshair.hit.distance != 0)
        {
            return;
        }
        else
        {
            AddArcEffect(projectile);
        }
    }

    private void AddArcEffect(GameObject gameObject)
    {
        iTween.PunchPosition(gameObject, new Vector3(
            Random.Range(-arcRange - 3f, arcRange + 3f),
            Random.Range(0.25f, arcRange + 0.25f), 0),
            Random.Range(minArcTime, maxArcTime));
    }
}

