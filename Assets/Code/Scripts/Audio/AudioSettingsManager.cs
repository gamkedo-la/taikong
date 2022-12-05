using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

public class AudioSettingsManager
{
    // [SerializeField] AudioMixer masterMixer;
    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]

    static void OnSceneLoad()
    {
        // On any scene load, set based on saved preferences
        ApplyAudioPreferences();        
        // Add listener if it's not there already!
        // if (!GameObject.Find(MUTEMANAGER_NAME))
        // {
        //     var go = new GameObject(MUTEMANAGER_NAME);
        //     go.AddComponent<MuteManager>();
        //     Object.DontDestroyOnLoad(go);
        //     go.CompareTag("fdsafasdf");
        // }
    }

    private static void ApplyAudioPreferences()
    {
        AudioListener.pause = GetMuted();
        Debug.Log("Applying audio settings");
    }
    // const string MUTEMANAGER_NAME = "MuteManager";

    public static void SaveSettings(bool muted, float volume, float sfx)
    {
        
        PlayerPrefsE.SetBool(MUTED_KEY, muted);
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.SetFloat(SFX_KEY, volume);

        PlayerPrefs.Save();
        
        ApplyAudioPreferences();
    }

    private const string MUTED_KEY = "muted";
    private const bool MUTED_DEFAULT = false;

    private const string VOLUME_KEY = "volume";
    private const float VOLUME_DEFAULT = 1.0f;

    private const string SFX_KEY = "sfx";
    private const float SFX_DEFAULT = 1.0f;

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(VOLUME_KEY, VOLUME_DEFAULT); // 0 to 1.9
    }

    public static float GetSfxVolume()
    {
        return PlayerPrefs.GetFloat(SFX_KEY, SFX_DEFAULT); // 0 to 1.9
    }

    public static bool GetMuted()
    {
        return PlayerPrefsE.GetBool(MUTED_KEY, MUTED_DEFAULT);
    }
}

// We don't have a way to persist booleans, and since PlayerPrefs is all statics, we can't make an extension
public static class PlayerPrefsE
{
    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }
    
    public static bool GetBool(string key, bool defaultValue)
    {
        return PlayerPrefs.GetInt(key) != 0;
    }
}