using UnityEngine;

public class UniversalAttraction_U : MonoBehaviour
{
    [SerializeField] private int killedEnemiesToTrigger;
    private byte kills = 0;
    private BoxCollider globalMagnet;

    private void Start()
    {
        globalMagnet = GameObject.FindGameObjectWithTag("Magnet").GetComponent<BoxCollider>();
    }

    public void OnEnemyKilled(Component sender, object data) 
    {
        kills++;
        if (kills >= killedEnemiesToTrigger)
        {
            EnableAbility();
            Invoke("DisableAbility", 2f);
            kills = 0;
        }
    }

    private void EnableAbility() 
    {
        globalMagnet.enabled = true;
    }

    private void DisableAbility() 
    {
        globalMagnet.enabled = false;
    }
}
