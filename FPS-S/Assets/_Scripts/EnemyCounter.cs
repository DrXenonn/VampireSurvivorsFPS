using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [HideInInspector] public int killedEnemies;
    [SerializeField] private GameEvent updateUI;
    public void UpdateKillCount()
    {
        killedEnemies++;
        updateUI.Raise();
    }
}
