using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]

public class AIManager : MonoBehaviour
{
    public Transform target;
    public List<AIUnit> units = new List<AIUnit>();

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer() 
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].MoveTo(target.position);
        }
    }
}