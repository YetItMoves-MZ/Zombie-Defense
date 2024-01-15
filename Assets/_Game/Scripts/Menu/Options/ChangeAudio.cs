using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    private float masterVolume;
    private float soundEffectsVolume;
    private float musicVolume;
    // Start is called before the first frame update
    void Start()
    {
        Options.FirstTimeUpdate();

        masterVolume = Options.MasterVolume;
        soundEffectsVolume = Options.SoundEffectsVolume;
        musicVolume = Options.MusicVolume;
        SetVolume(masterVolume, "MasterVolume");
        SetVolume(soundEffectsVolume, "SoundEffectsVolume");
        SetVolume(musicVolume, "MusicVolume");

    }

    // Update is called once per frame
    void Update()
    {
        if (Options.MasterVolume != masterVolume)
        {
            masterVolume = Options.MasterVolume;
            SetVolume(masterVolume, "MasterVolume");
        }
        if (Options.SoundEffectsVolume != soundEffectsVolume)
        {
            soundEffectsVolume = Options.SoundEffectsVolume;
            SetVolume(soundEffectsVolume, "SoundEffectsVolume");
        }
        if (Options.MusicVolume != musicVolume)
        {
            musicVolume = Options.MusicVolume;
            SetVolume(musicVolume, "MusicVolume");
        }
    }

    void SetVolume(float volume, string kind)
    {
        // audio mixer works with DBs so this translates to DB
        mixer.SetFloat(kind, Mathf.Log10(volume) * 20);
    }
}
