using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Steamworks;

public class StoreAPI_Steam : StoreAPI
{
    // private static uint APP_ID = 612040;

    public override void Initialize()
    {
        // try
        // {
        //     SteamClient.Init(APP_ID, true);
        // }
        // catch (Exception e)
        // {
        //     Debug.LogError(e.Message);
        // }
    }

    public override SystemLanguage GetCurrentLanguage()
    {
        // string lang = "english";

        // if (SteamClient.IsValid)
        // {
        //     lang = SteamApps.GameLanguage;
        // }

        // return Localization_Helper.GetLanguageFromName(lang);
        return SystemLanguage.English; 
    }

    public override void GiveAchievement(eAchievementID achievementID)
    {
        // Debug.Log("AWARDING ACHIEVEMENT: " + achievementID);
        
        // if (SteamClient.IsValid)
        // {
        //     foreach (Steamworks.Data.Achievement ach in SteamUserStats.Achievements)
        //     {
        //         if (ach.Identifier == achievementID.ToString() && ach.State == false)
        //         {
        //             ach.Trigger();
        //         }
        //     }
        // }
    }

    public override void Shutdown()
    {
        // Debug.Log("SHUTTING DOWN STEAM API");
        // SteamClient.Shutdown();
    }

    private bool IsValid()
    {
        // return SteamClient.IsValid;
        return false;
    }
}
