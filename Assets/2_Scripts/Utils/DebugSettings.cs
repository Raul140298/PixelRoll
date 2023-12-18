using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSettings : MonoBehaviour
{
    private static DebugSettings instance;

    [SerializeField]
    private bool enableDebugSettings;

    [Header("TIME")]
    [SerializeField]
    private float deltaTime;

    [Header("LANGUAGE")]
    [SerializeField]
    private bool shouldForceLanguage;

    [SerializeField]
    private SystemLanguage forcedLanguage;

    void Awake()
    {
        instance = this;
    }

    public bool IsForcingLanguage()
    {
        return (enableDebugSettings && shouldForceLanguage);
    }

    public SystemLanguage GetForcedLanguage()
    {
        return forcedLanguage;
    }

    public static DebugSettings Instance
    {
        get { return instance; }
    }
}
