using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClass", menuName = "Class", order = 3)]

public class Classes : ScriptableObject
{
    public string name;
    public string description;
    public int startingPrimaryAD;
    public Color color;
    public List<GameObject> projectile;
    public Mesh wandMesh;
    public List<UpgradeData> upgrades;
}
