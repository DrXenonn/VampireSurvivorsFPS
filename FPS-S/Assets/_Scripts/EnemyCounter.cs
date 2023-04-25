using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [HideInInspector] public int killedEnemies;
    public void UpdateKillCount()
    {
        killedEnemies++;
    }
}
