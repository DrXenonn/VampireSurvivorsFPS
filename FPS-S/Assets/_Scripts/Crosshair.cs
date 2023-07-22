using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public static Crosshair Instance { get; private set; }

    [SerializeField] private Image cursorImage;
    [SerializeField] private float hitRange;
    [SerializeField] private Color hitColor, noHitColor;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask notEnemyLayer;
    [HideInInspector] public Vector3 destination;
    [HideInInspector] public RaycastHit hit;

    private Ray ray;
    private new Camera camera;

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
        camera = Camera.main;
    }

    public Vector3 GetDirection()
    {
        return (destination - Shooting.Instance.firePos.position).normalized;
    }

    private void Update()
    {
        RayCastCheckForHit();
    }

    private void RayCastCheckForHit()
    {
        ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit, hitRange, enemyLayer))
        {
            cursorImage.color = hitColor;
            destination = hit.point;

        }
        else if (Physics.Raycast(ray, out hit, hitRange, notEnemyLayer))
        {
            destination = hit.point;
            cursorImage.color = noHitColor;
        }
        else
        {
            destination = ray.GetPoint(1000f);
            cursorImage.color = noHitColor;
        }
    }
}
