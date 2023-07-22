using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Electromancy_U : MonoBehaviour
{
    [SerializeField] private GameObject lightning;
    public int numberOfClosestEnemies = 3;
    [SerializeField] private float radius;
    [SerializeField] private float lightningTtl;
    [SerializeField] private int lightningDamage;
    [SerializeField] private Mesh electroWand;
    [ColorUsage(true, true)]
    [SerializeField] private Color color, emissionColor;
    [SerializeField] private int projectileSpeed;

    private bool isAbilityOnCooldown = false;
    [SerializeField] private float abilityCooldownDuration = 5f;

    private void Start()
    {
        var wand = GameObject.FindGameObjectWithTag("Wand").GetComponent<MeshFilter>();
        wand.mesh = electroWand;
        Shooting.Instance.arc = false;
        PlayerStats.Instance.projectileSpeed = projectileSpeed;
        MaterialColorController.instance.SetNewColors(color, emissionColor);
    }

    public void LightningOnHit(Component sender, object data)
    {
        if (isAbilityOnCooldown)
        {
            return;
        }

        var hitObject = (GameObject)data;

        var closestEnemies = FindClosestEnemies(numberOfClosestEnemies, radius, hitObject);
        var damage = Mathf.FloorToInt(lightningDamage * PlayerStats.Instance.elementalDamageMultiplier);

        // If we have at least one closest enemy, create lightning effect between hit enemy and the closest one
        if (closestEnemies.Count > 0)
        {
            var closestEnemy = closestEnemies[0];
            var lightningPrefab = Instantiate(lightning);
            lightningPrefab.transform.GetChild(0).position = hitObject.transform.position;
            lightningPrefab.transform.GetChild(1).position = closestEnemy.transform.position;
            Destroy(lightningPrefab, lightningTtl);

            // Deal damage to the closest enemy hit by the lightning
            closestEnemy.GetComponent<EnemyHP>().TakeDamage(damage, "Lightning");
        }

        // Loop through each pair of closest enemies (starting from index 1) and create a lightning effect between them
        for (int i = 1; i < closestEnemies.Count; i++)
        {
            for (int j = i + 1; j < closestEnemies.Count; j++)
            {
                var lightningPrefab = Instantiate(lightning);
                lightningPrefab.transform.GetChild(0).position = closestEnemies[i].transform.position;
                lightningPrefab.transform.GetChild(1).position = closestEnemies[j].transform.position;
                Destroy(lightningPrefab, lightningTtl);

                // Deal damage to each enemy hit by the lightning effect
                closestEnemies[i].GetComponent<EnemyHP>().TakeDamage(Mathf.FloorToInt(damage), "Lightning");
                closestEnemies[j].GetComponent<EnemyHP>().TakeDamage(damage, "Lightning");
            }
        }
        
        isAbilityOnCooldown = true;
        StartCoroutine(CooldownTimer());
    }

    private IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(abilityCooldownDuration);
        isAbilityOnCooldown = false;
    }

    private List<GameObject> FindClosestEnemies(int numberOfEnemies, float range, GameObject hitObject)
    {
        var closestEnemies = new List<GameObject>();
        var colliders = Physics.OverlapSphere(hitObject.transform.position, range);

        foreach (var col in colliders)
        {
            if (!col.CompareTag("Enemy") || col.gameObject == hitObject) continue;
            
            var distance = Vector3.Distance(hitObject.transform.position, col.transform.position);
            
            var insertIndex = 0;
            for (var i = 0; i < closestEnemies.Count; i++)
            {
                if (distance < Vector3.Distance(hitObject.transform.position, closestEnemies[i].transform.position))
                {
                    insertIndex = i;
                    break;
                }
            }

            closestEnemies.Insert(insertIndex, col.gameObject);

            // If we have found more enemies than required, remove the farthest ones
            if (closestEnemies.Count > numberOfEnemies)
                closestEnemies.RemoveAt(numberOfEnemies);
        }

        return closestEnemies;
    }
}
