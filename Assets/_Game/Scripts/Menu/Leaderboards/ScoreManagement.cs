using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManagement
{
    public static int ZombiesKilled;
    public static int EnemyBuildingsDestroyed;
    public static bool NewHighScore;
    public static int TotalScore;
    public static int HighScoreCount;

    public static string GameEndedMessage;

    static List<Stats> enemyBuildingsList = new List<Stats>();

    public static void StartNewGame()
    {
        enemyBuildingsList = new List<Stats>();
        ZombiesKilled = 0;
        EnemyBuildingsDestroyed = 0;
        NewHighScore = false;
        HighScoreCount = 0;
        GameEndedMessage = "";
    }

    public static void CheckNewHighScore()
    {
        TotalScore = EnemyBuildingsDestroyed * 100 + ZombiesKilled;
        int scoreAmount = PlayerPrefs.GetInt("PeopleScored", 0);
        if (scoreAmount == 0)
        {
            NewHighScore = true;
            HighScoreCount = 1;
            return;
        }
        for (int i = 1; i <= scoreAmount; i++)
        {
            if (TotalScore > PlayerPrefs.GetInt("TotalScore" + i, 0))
            {
                NewHighScore = true;
                HighScoreCount = i;
                return;
            }
        }
        if (scoreAmount < 3)
        {
            HighScoreCount = scoreAmount + 1;
            NewHighScore = true;
        }
    }

    public static void AddEnemyBuilding(Stats enemyBuilding)
    {
        enemyBuildingsList.Add(enemyBuilding);
    }

    public static void RemoveEnemyBuilding(Stats enemyBuilding)
    {
        enemyBuildingsList.Remove(enemyBuilding);
        if (enemyBuildingsList.Count == 0)
        {
            CheckNewHighScore();
            GameEndedMessage = "You Win";
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}
