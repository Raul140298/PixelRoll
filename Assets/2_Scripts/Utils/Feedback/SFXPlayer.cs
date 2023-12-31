﻿using UnityEngine;

public static class SFXPlayer
{
    public static void PlaySFX(eFeedbackType sfx)
    {
        if (AudioController.Instance != null)
        {
            string sfxName = sfx.ToString();

            if (AudioController.IsValidAudioID(sfxName))
            {
                AudioController.Play(sfxName);
            }
            else
            {
                Debug.LogError("SFX NOT FOUND: " + sfxName);
            }
        }
    }
    
    public static void PlayBGM(eFeedbackType sfx)
    {
        if (AudioController.Instance != null)
        {
            string sfxName = sfx.ToString();

            if (AudioController.IsValidAudioID(sfxName))
            {
                AudioController.PlayMusic(sfxName);
            }
            else
            {
                Debug.LogError("BGM NOT FOUND: " + sfxName);
            }
        }
    }
}
