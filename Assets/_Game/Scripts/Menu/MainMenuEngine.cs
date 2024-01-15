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
        // TODO check high scores
        // if (InGameUIEngine.needToCheckHighScore)
        // {
        //     InGameScoreManagement.CheckNewHighScore();
        //     if (InGameScoreManagement.newHighScore)
        //     {
        //         // Load high score scene
        //         SceneManager.LoadScene(1, LoadSceneMode.Additive);
        //     }

        // }


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExitClick();

        MainMenuUI.SetActive(!Options.IsOptionsMenuLoaded);
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
        // TODO make leaderboards...
        // Load leaderboards
        // SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void OnOptionsClick()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }
}
