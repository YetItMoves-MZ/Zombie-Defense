using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEngine : MonoBehaviour
{
    [SerializeField] GameObject MainMenuUI;
    void Start()
    {
        if (ScoreManagement.NewHighScore)
        {
            SceneManager.LoadScene(4, LoadSceneMode.Additive);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExitClick();

        bool optionsIsOpened = OptionsMenuEngine.Instance != null && OptionsMenuEngine.Instance.IsOpened;
        bool leaderboardsIsOpened = LeaderboardsEngine.Instance != null && LeaderboardsEngine.Instance.IsOpened;
        MainMenuUI.SetActive(!(optionsIsOpened || leaderboardsIsOpened));
    }
    public void OnPlayClick()
    {
        ScoreManagement.StartNewGame();
        // Load game scene
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void OnExitClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void OnLeaderboardsClick()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
    }

    public void OnOptionsClick()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }
}
