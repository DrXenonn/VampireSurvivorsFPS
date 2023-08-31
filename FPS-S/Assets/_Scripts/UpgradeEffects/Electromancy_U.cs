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
    [SerializeField] private GameEvent onClassChoose;
    [ColorUsage(true, true)]
    [SerializeField] private Color color, emissionColor;
    [SerializeField] private int projectileSpeed;

    private bool isAbilityOnCooldown = false;
    [SerializeField] private float abilityCooldownDuration = 5f;

    private void Start()
    {
        onClassChoose.Raise("electromancy");
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
        
        for (var i = 0; i < closestEnemies.Count; i++)
        {
            var lightningPrefab = Instantiate(lightning);
            lightningPrefab.transform.GetChild(0).position = hitObject.transform.position;
            lightningPrefab.transform.GetChild(1).position = closestEnemies[i].transform.position;
            var position = lightningPrefab.transform.position;
            position = new Vector3(position.x,position.y + 0.2f, position.z);
            lightningPrefab.transform.position = position;
            Destroy(lightningPrefab, lightningTtl);
            
            closestEnemies[i].GetComponent<EnemyHP>().TakeDamage(Mathf.FloorToInt(damage), "Lightning");
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
            
            if (closestEnemies.Count > numberOfEnemies)
                closestEnemies.RemoveAt(numberOfEnemies);
        }

        return closestEnemies;
    }
}
