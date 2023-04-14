using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image cursorImage;
    [SerializeField] private float hitRange;
    [SerializeField] private Color hitColor, noHitColor;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask notEnemyLayer;
    [HideInInspector] public Vector3 destination;
    [HideInInspector] public RaycastHit hit;
    private Ray ray;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
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
