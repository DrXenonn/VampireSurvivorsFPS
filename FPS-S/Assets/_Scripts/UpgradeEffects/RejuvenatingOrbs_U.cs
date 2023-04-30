using UnityEngine;

public class RejuvenatingOrbs_U : MonoBehaviour
{
    [SerializeField] private GameEvent onRestoreMana;
    [SerializeField] private float restoreChance;

    public void onAnyXpPickup(Component sender, object data)
    {
        if (Random.value < restoreChance)
        {
            onRestoreMana.Raise(999);
        }
    }
}
