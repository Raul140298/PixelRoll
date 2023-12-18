using UnityEngine;

public static class Creator
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnLoad()
    {
        if (GameObject.FindObjectOfType<SceneLoader>() == null)
        {
            GameObject goSceneLoader = new GameObject("SceneLoader", typeof(SceneLoader));
            goSceneLoader.name = "DDOL_SceneLoader";
            GameObject.DontDestroyOnLoad(goSceneLoader);
        }
        
        if (GameObject.FindObjectOfType<AudioManager>() == null)
        {
            GameObject audioManagerPrefab = Resources.Load("Prefabs/Audio/PFB_AudioController") as GameObject;
            GameObject goAudioManager = GameObject.Instantiate(audioManagerPrefab);
            goAudioManager.name = "DDOL_AudioController";
            GameObject.DontDestroyOnLoad(goAudioManager);
        }
    }
}
