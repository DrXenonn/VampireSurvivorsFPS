using System;
using UnityEngine;

public class MainMenuDiscordManager : MonoBehaviour {

    private Discord.Discord discord;
    private int chaosLevel;
    
    void Start ()
    {
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
            State = "Main Menu",
            Assets =
            {
                LargeImage = "icon"
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