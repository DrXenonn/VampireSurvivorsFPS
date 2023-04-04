using UnityEngine;

public abstract class Upgrade
{
    public string name;
    public string description;
    public int levelRequirement;
    public Sprite icon;

    public virtual void ApplyUpgrade()
    {
        // Base upgrade effect
    }
}
