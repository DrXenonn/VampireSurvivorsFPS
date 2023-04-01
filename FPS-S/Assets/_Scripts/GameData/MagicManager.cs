using UnityEngine;

public enum Magic
{
    Fire,
    Ice,
    Lightning,
}

public class MagicManager : MonoBehaviour
{
    [Header("Fire")]
    [SerializeField] GameObject firePrefab;

    [Header("Ice")]
    [SerializeField] GameObject icePrefab;

    [Header("Lightning")]
    [SerializeField] GameObject lightningPrefab;


    [HideInInspector] public GameObject currentPrefab;
    public static MagicManager Instance { get; private set; }

    public Magic CurrentMagic { get; set; }

    private void Awake()
    {
        Instance = this;
        MagicManager.Instance.CurrentMagic = Magic.Fire;

        switch (MagicManager.Instance.CurrentMagic)
        {
            case Magic.Fire:
                currentPrefab = firePrefab;

                break;
            case Magic.Ice:
                currentPrefab = icePrefab;
                break;

            default:
                break;
        }
    }
}

