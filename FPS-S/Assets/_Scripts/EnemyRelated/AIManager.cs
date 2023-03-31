using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]

public class AIManager : MonoBehaviour
{
    public Transform target;
    public List<AIUnit> units = new List<AIUnit>();

    private void Update()
    {
        Invoke("MakeAgentsCircleTarget", 0.1f);
    }

    private void MakeAgentsCircleTarget()
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].MoveTo(new Vector3(
                GetTargetPos().x + GetRadiusAroundTarget(i) * Mathf.Cos(2 * Mathf.PI * i / units.Count),
                GetTargetPos().y,
                GetTargetPos().z + GetRadiusAroundTarget(i) * Mathf.Sin(2 * Mathf.PI * i / units.Count)));
        }
    }

    Vector3 GetTargetPos()
    {
        return target.transform.position;
    }

    float GetRadiusAroundTarget(int i)
    {
        return units[i].enemyManager.radiusAroundTarget;
    }
}