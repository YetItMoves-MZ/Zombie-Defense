using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManagement
{
    public static int zombiesKilled;
    public static int enemyBuildingsDestroyed;
    public static bool newHighScore;
    public static int highScoreCount;

    public static string GameEndedMessage;

    static List<Stats> enemyBuildingsList = new List<Stats>();

    public static void StartNewGame()
    {
        enemyBuildingsList = new List<Stats>();
        zombiesKilled = 0;
        enemyBuildingsDestroyed = 0;
        newHighScore = false;
        highScoreCount = 0;
        GameEndedMessage = "";
    }

    public static void CheckNewHighScore()
    {
        int totalScore = enemyBuildingsDestroyed * 100 + zombiesKilled;
        int scoreAmount = PlayerPrefs.GetInt("PeopleScored", 0);
        if (scoreAmount == 0)
        {
            newHighScore = true;
            highScoreCount = 1;
            return;
        }
        for (int i = 1; i <= scoreAmount; i++)
        {
            if (totalScore > PlayerPrefs.GetInt("TotalScore" + i, 0))
            {
                newHighScore = true;
                highScoreCount = i;
                return;
            }
        }
        if (scoreAmount < 3)
        {
            highScoreCount = scoreAmount + 1;
            newHighScore = true;
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
            GameEndedMessage = "You Win";
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}
