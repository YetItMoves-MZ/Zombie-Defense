using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuEngine : MonoBehaviour
{
    public static OptionsMenuEngine Instance { get; private set; }
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider soundEffectsVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] GameObject SurrenderButton;

    [HideInInspector] public bool IsOpened;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SurrenderButton.SetActive(SceneManager.GetSceneByBuildIndex(1).isLoaded);
        IsOpened = true;
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
        IsOpened = false;
    }

    public void OnSurrenderClick()
    {
        ScoreManagement.CheckNewHighScore();
        ScoreManagement.GameEndedMessage = "You Lose";
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        OnCloseClick();
    }
}
