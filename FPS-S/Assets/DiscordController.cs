using System;
using Unity.VisualScripting;
using UnityEngine;

public class DiscordController : MonoBehaviour {

    private Discord.Discord discord;
    
    private int chaosLevel;
    private int playerLevel = 1;
    private string playerClass;
    
    void Start ()
    {
        chaosLevel = PlayerPrefs.GetInt("Chaos", 1);
        try
        {
            discord = new Discord.Discord
            (
                1143540078375862344,
                (UInt64)Discord.CreateFlags.NoRequireDiscord
            );
        }
        catch(Discord.ResultException)
        {
            discord = null;
        }

        var activityManager = discord?.GetActivityManager();

        var activity = new Discord.Activity
        {
            State = $"Chaos Level: {chaosLevel}",
            Details = "Level: 1",
            Assets =
            {
                LargeImage = "icon",
                LargeText = "Sandcaster"
                
            }
        };
        activityManager?.UpdateActivity(activity, result => { });
    }

    public void OnLevelUp(Component sender, object data)
    {
        playerLevel = (int)data;
        
        var activityManager = discord?.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = $"Chaos Level: {chaosLevel}",
            Details = $"Level: {playerLevel}",
            Assets =
            {
                LargeImage = "icon",
                LargeText = "Sandcaster",
                SmallImage = playerClass,
                SmallText = playerClass.FirstCharacterToUpper()
            }
        };
        activityManager?.UpdateActivity(activity, result => { });
    }

    public void OnChooseClass(Component sender, object data)
    {
        playerClass = (string)data;
        var activityManager = discord?.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = $"Chaos Level: {chaosLevel}",
            Details = $"Level: {playerLevel}",
            Assets =
            {
                LargeImage = "icon",
                LargeText = "Sandcaster",
                SmallImage = playerClass,
                SmallText = playerClass.FirstCharacterToUpper()
            }
        };
        activityManager?.UpdateActivity(activity, result => { });
    }

    private void Update () 
    {
        try
        {
            discord?.RunCallbacks();
        }
        catch
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        discord?.Dispose();
    }
}