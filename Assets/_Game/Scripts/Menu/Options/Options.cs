using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Options
{
    public static float MasterVolume;
    public static float SoundEffectsVolume;
    public static float MusicVolume;
    public static bool IsOptionsMenuLoaded = false;

    public static void FirstTimeUpdate()
    {
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", .5f);
        SoundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume", .5f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", .5f);


    }
}
