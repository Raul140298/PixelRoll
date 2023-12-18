using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using GameAnalyticsSDK;

public static class AnalyticsWrapper
{
    public static void Initialize()
    {
        // Debug.Log("Analytics Initialize");
        // GameAnalytics.Initialize();
    }

    public static void RegisterTutorialStart()
    {
		// Debug.Log("Tutorial Start");
        // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "tutorial");
    }

    public static void RegisterTutorialEnd()
    {
		// Debug.Log("Tutorial End");
        // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "tutorial");
    }

    public static void RegisterMatchStart()
    {
        // string eventName = "match_start";
        // Debug.Log("Registering " + eventName);
        // GameAnalytics.NewDesignEvent(eventName);
    }

    public static void RegisterMatchEnd()
    {
		// string eventName = "match_end";
		// Debug.Log("Registering " + eventName + " / " + Time.timeSinceLevelLoad);
        // GameAnalytics.NewDesignEvent(eventName, Time.timeSinceLevelLoad);
    }

    public static void RegisterSessionStart()
    { 
        // Debug.Log("Session Start");
        // GameAnalytics.NewDesignEvent("session_start");
    }

    public static void RegisterSessionEnd()
    {
		// Debug.Log("Session End");
        // GameAnalytics.NewDesignEvent("session_end", Time.realtimeSinceStartup);
    }
}
