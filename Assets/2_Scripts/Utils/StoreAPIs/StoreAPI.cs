using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreAPI : MonoBehaviour
{
    private static StoreAPI instance;

    public void AwakeComponent()
    {
        instance = this;
    }

    public virtual void Initialize()
    { 

    }

    public virtual SystemLanguage GetCurrentLanguage()
    {
        return SystemLanguage.English;
    }

    public virtual void GiveAchievement(eAchievementID achievementID)
    { 
        
    }

    public virtual void Shutdown()
    { 

    }

    public static StoreAPI Instance
    {
        get { return instance; }
    }
}
