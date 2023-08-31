using UnityEngine;

public class RejuvenatingOrbs_U : MonoBehaviour
{
    [SerializeField] private GameEvent onRestoreMana;
    [SerializeField] private float restoreChance;
    [SerializeField] private int manaToFill;

    public void OnAnyXpPickup(Component sender, object data)
    {
        if (Random.value < restoreChance)
        {
            onRestoreMana.Raise(manaToFill);
        }
    }
}
