using TMPro;
using UnityEngine;

public class DamagePopupAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve opacityCurve;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private AnimationCurve heightCurve;
    private TextMeshProUGUI tmp;
    private float time = 0f;
    private Vector3 origin;
    private bool isActive = false;

    private void Awake()
    {
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        origin = transform.position;
        isActive = true;
        time = 0f;
        UpdateAnimation();
    }

    private void OnDisable()
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {
            time += Time.deltaTime;
            UpdateAnimation();
        }
    }

    private void UpdateAnimation()
    {
        tmp.color = new Color(1, 1, 1, opacityCurve.Evaluate(time));
        transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
        transform.position = origin + new Vector3(0, 1 + heightCurve.Evaluate(time), 0);
    }
}