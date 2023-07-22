using UnityEngine;

public class ChangeSecondary_U : MonoBehaviour
{
    private enum Modes
    {
        None,
        Burst,
        AllIn
    }
    [SerializeField] private Modes modes;

    private void Start()
    {
        var shootingScript = Shooting.Instance;
        
        switch (modes)
        {
            case Modes.None:
                shootingScript.secondaryMode = Shooting.SecondaryMode.None;
                break;
            case Modes.Burst:
                shootingScript.secondaryMode = Shooting.SecondaryMode.Burst;
                break;
            case Modes.AllIn:
                shootingScript.secondaryMode = Shooting.SecondaryMode.AllIn;
                break;
        }
        
        Destroy(gameObject);
    }
}