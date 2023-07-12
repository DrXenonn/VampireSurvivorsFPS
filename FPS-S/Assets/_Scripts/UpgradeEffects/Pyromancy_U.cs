using System.Collections;
using UnityEngine;

public class Pyromancy_U : MonoBehaviour
{
    [SerializeField] private float ticks;
    [SerializeField] private float tickRate;
    [SerializeField] private int tickDamage;
    [SerializeField] private float burnChance;
    [SerializeField] private GameObject fireParticles;

    public void BurnOnHit(Component sender, object data)
    {
        if (!(Random.value < burnChance)) return;
        
        var objectToBurn = (GameObject)data;
        var enemyHp = objectToBurn.GetComponent<EnemyHP>();

        if (!enemyHp.isDeBuffApplied)
        {
            StartCoroutine(nameof(Burn), enemyHp);
            enemyHp.isDeBuffApplied = true;
        }
    }

    private IEnumerator Burn(EnemyHP enemyHp)
        {
            var enemyTransform = enemyHp.gameObject.transform;
            var enemyPos = enemyTransform.position;
            enemyPos = new Vector3(enemyPos.x, enemyPos.y + 0.55f, enemyPos.z);

            var fire = Instantiate(fireParticles, enemyPos, Quaternion.identity, enemyTransform.transform);
            Destroy(fire, tickRate * ticks);

            for (var i = 0; i < ticks; i++)
            {
                if (!enemyHp.gameObject.activeSelf) //If enemy died
                {
                    enemyHp.isDeBuffApplied = false;
                    Destroy(fire);
                    StopCoroutine(nameof(Burn));
                }

                yield return new WaitForSeconds(tickRate);
                enemyHp!.TakeDamage(tickDamage, false, true);
            }

            //make Enemy Burnable after the effect is done
            enemyHp.isDeBuffApplied = false;
        }
    }