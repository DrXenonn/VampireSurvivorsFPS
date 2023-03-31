using UnityEngine.UI;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private ClassHandler classHandler;
    [SerializeField] private Transform firePos;
    [SerializeField] private Image cursorImage;
    [SerializeField] private Sprite hitSprite, noHitSprite;
    [SerializeField] private Mana manaManager;
    [SerializeField] Animator staffAnimator;
    [Header("Variables")]
    [SerializeField] private float hitRange;
    [SerializeField] private Color hitColor, noHitColor;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask notEnemyLayer;
    [SerializeField] private float arcRange;
    [SerializeField] private float projectileCount;
    [SerializeField] private float minArcTime, maxArcTime;
    [SerializeField] private float minimumDistanceToArc;
    [SerializeField] private int primaryFireManaCost;
    [SerializeField] private int secondaryFireManaCost;

    private Vector3 destination;
    private Ray ray;
    private RaycastHit hit;
    private float timeToFire;

    private void Update()
    {
        RayCastCheckForHit();
        ShootPrimaryFire();
        ShootSecondaryFire();
    }

    private void RayCastCheckForHit()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit, hitRange, enemyLayer))
        {
            cursorImage.sprite = hitSprite;
            cursorImage.color = hitColor;
            destination = hit.point;
        }
        else if (Physics.Raycast(ray, out hit, hitRange, notEnemyLayer))
        {
            destination = hit.point;
            cursorImage.sprite = noHitSprite;
            cursorImage.color = noHitColor;
        }
        else 
        {
            destination = ray.GetPoint(1000f);
            cursorImage.sprite = noHitSprite;
            cursorImage.color = noHitColor;
        }
    }

    private void ShootPrimaryFire()
    {
        if (Input.GetMouseButton(0) && Time.time >= timeToFire && manaManager.mana >= primaryFireManaCost)
        {
            manaManager.mana -= primaryFireManaCost;
            timeToFire = Time.time + 1 / playerStats.fireRate;
            staffAnimator.SetTrigger("Shoot");
            InstantiateProjectile();
        }
    }

    private void ShootSecondaryFire()
    {
        if (Input.GetMouseButton(1) && Time.time >= timeToFire && manaManager.mana >= secondaryFireManaCost)
        {
            manaManager.mana -= secondaryFireManaCost;
            timeToFire = Time.time + 1 / playerStats.fireRate;
            staffAnimator.SetTrigger("Shoot");
            for (int i = 0; i < projectileCount; i++)
            {
                InstantiateProjectile();
            }
        }
    }

    private void InstantiateProjectile()
    {
        var projectile = Instantiate(classHandler.chosenClass.projectile[0], firePos.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody>().velocity = ((destination - firePos.position).normalized * playerStats.projectileSpeed);

        if (hit.distance < minimumDistanceToArc && hit.distance != 0)
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

