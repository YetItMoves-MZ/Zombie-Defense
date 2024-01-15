using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuEngine : MonoBehaviour
{
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider soundEffectsVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        Options.IsOptionsMenuLoaded = true;
        masterVolumeSlider.value = Options.MasterVolume;
        soundEffectsVolumeSlider.value = Options.SoundEffectsVolume;
        musicVolumeSlider.value = Options.MusicVolume;
    }

    public void SaveChanges(int index)
    {
        if (index == 1)
        {
            PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
            Options.MasterVolume = masterVolumeSlider.value;
        }
        if (index == 2)
        {
            PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
            Options.MusicVolume = musicVolumeSlider.value;
        }
        if (index == 3)
        {
            PlayerPrefs.SetFloat("SoundEffectsVolume", soundEffectsVolumeSlider.value);
            Options.SoundEffectsVolume = soundEffectsVolumeSlider.value;
        }
    }

    public void OnCloseClick()
    {
        SceneManager.UnloadSceneAsync(3);
    }

    private void OnDestroy()
    {
        Options.IsOptionsMenuLoaded = false;
    }
}
