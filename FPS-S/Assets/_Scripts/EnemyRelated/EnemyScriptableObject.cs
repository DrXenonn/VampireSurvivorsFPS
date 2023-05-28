using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public string enemyName;
    public float enemyMovementSpeed;
    public int enemyHealth;
    public int enemyDamage;
    public float radiusAroundTarget;
    public GameObject xpDropPrefab;
    public float attackRange;
}
