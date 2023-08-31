using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Pyromancy_U : MonoBehaviour
{
    [SerializeField] private float ticks;
    [SerializeField] private float tickRate;
    [SerializeField] private int tickDamage;
    [SerializeField] private float burnChance;
    [SerializeField] private GameObject fireParticles;
    [SerializeField] private Mesh fireWand;
    [SerializeField] private GameEvent onClassChoose;
    [ColorUsage(true,true)]
    [SerializeField] private Color color, emissionColor;

    private void Start()
    {
        onClassChoose.Raise("pyromancy");
        var wand = GameObject.FindGameObjectWithTag("Wand").GetComponent<MeshFilter>();
        wand.mesh = fireWand;
        MaterialColorController.instance.SetNewColors(color, emissionColor);
    }
    

    public void BurnOnHit(Component sender, object data)
    {
        if (!(Random.value < burnChance)) return;

        var objectToBurn = (GameObject)data;
        var enemyHp = objectToBurn.GetComponent<EnemyHP>();

        if (!enemyHp.isDeBuffApplied)
        {
            StartCoroutine(Burn(enemyHp));
            enemyHp.isDeBuffApplied = true;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Burn(EnemyHP enemyHp)
    {
        var enemyTransform = enemyHp.gameObject.transform;
        var enemyBounds = enemyTransform.GetComponent<Collider>().bounds;

        var centerPosition = enemyBounds.center;
        
        var fire = Instantiate(fireParticles, centerPosition, Quaternion.identity, enemyTransform);
        Destroy(fire, tickRate * ticks);

        for (var i = 0; i < ticks; i++)
        {
            if (!enemyHp.gameObject.activeSelf)
            {
                enemyHp.isDeBuffApplied = false;
                Destroy(fire);
                yield break;
            }

            yield return new WaitForSeconds(tickRate);
            enemyHp.TakeDamage(Mathf.FloorToInt(tickDamage * PlayerStats.Instance.elementalDamageMultiplier), "Burn");
        }
        
        enemyHp.isDeBuffApplied = false;
    }
}