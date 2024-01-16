using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LeaderboardsEngine : MonoBehaviour
{
    public static LeaderboardsEngine Instance { get; private set; }

    [HideInInspector] public bool IsOpened;

    [Header("Leaderboards")]
    public GameObject scorePrefab;
    public GameObject leaderboardsParentObject;

    [Header("New Score")]
    public GameObject highScoreNameObject;
    public TMP_Text highScoreName;
    public List<char> availableChars;
    List<GameObject> scores;
    public static bool createNewScore = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        IsOpened = true;

        scores = new List<GameObject>();
        if (ScoreManagement.NewHighScore)
            NewHighScore();
        else
            ShowScores();
    }

    private void ShowScores()
    {
        ClearUI();
        int scoreAmount = PlayerPrefs.GetInt("PeopleScored", 0);
        if (scoreAmount == 0)
            return;

        scores = new List<GameObject>();
        for (int i = 1; i <= scoreAmount; i++)
        {
            string name = PlayerPrefs.GetString("Name" + i, "TAV");
            int zombiesKilled = PlayerPrefs.GetInt("ZombiesKilled" + i, 0);
            int enemyBuildingsDestroyed = PlayerPrefs.GetInt("EnemyBuildingsDestroyed" + i, 0);
            int totalScore = PlayerPrefs.GetInt("TotalScore" + i, 0);
            scores.Add(CreateNewScoreObject(name, zombiesKilled, enemyBuildingsDestroyed, totalScore, i - 1));
        }
    }

    private GameObject CreateNewScoreObject(string name, int zombiesKilled, int enemyBuildingsDestroyed, int totalScore, int place)
    {
        GameObject newScore = Instantiate(scorePrefab, leaderboardsParentObject.transform);

        RectTransform transformComponent = newScore.GetComponent<RectTransform>();
        transformComponent.Translate(0f, -150f * (place - 1), 0f);

        newScore.transform.GetChild(0).GetComponent<TMP_Text>().text = name;
        newScore.transform.GetChild(1).GetComponent<TMP_Text>().text = zombiesKilled.ToString();
        newScore.transform.GetChild(2).GetComponent<TMP_Text>().text = enemyBuildingsDestroyed.ToString();
        newScore.transform.GetChild(3).GetComponent<TMP_Text>().text = totalScore.ToString();

        return newScore;
    }

    private void ClearUI()
    {
        foreach (GameObject scoreUI in scores)
        {
            Destroy(scoreUI);
        }
        scores.Clear();
    }

    private void NewHighScore()
    {
        ScoreManagement.NewHighScore = false;
        leaderboardsParentObject.SetActive(false);
        highScoreNameObject.SetActive(true);
    }

    public void OnNewHighScoreConfirmClick()
    {
        string name = "" + GetChar(1) + GetChar(2) + GetChar(3);

        int zombiesKilled = ScoreManagement.ZombiesKilled;
        int enemyBuildingsDestroyed = ScoreManagement.EnemyBuildingsDestroyed;
        int totalScore = ScoreManagement.TotalScore;

        int highScoreCount = ScoreManagement.HighScoreCount;


        for (int i = 3; i > highScoreCount; i--)
        {
            string currentPlaceName = PlayerPrefs.GetString("Name" + (i - 1));
            int currentPlaceZombiesKilled = PlayerPrefs.GetInt("ZombiesKilled" + (i - 1));
            int currentPlaceEnemyBuildingsDestroyed = PlayerPrefs.GetInt("EnemyBuildingsDestroyed" + (i - 1));
            int currentPlaceTotalScore = PlayerPrefs.GetInt("TotalScore" + (i - 1));

            PlayerPrefs.SetString("Name" + (i), currentPlaceName);
            PlayerPrefs.SetInt("ZombiesKilled" + (i), currentPlaceZombiesKilled);
            PlayerPrefs.SetInt("EnemyBuildingsDestroyed" + (i), currentPlaceEnemyBuildingsDestroyed);
            PlayerPrefs.SetInt("TotalScore" + (i), currentPlaceTotalScore);
        }

        PlayerPrefs.SetString("Name" + highScoreCount, name);
        PlayerPrefs.SetInt("ZombiesKilled" + highScoreCount, zombiesKilled);
        PlayerPrefs.SetInt("EnemyBuildingsDestroyed" + highScoreCount, enemyBuildingsDestroyed);
        PlayerPrefs.SetInt("TotalScore" + highScoreCount, totalScore);



        int totalPeopleScored = PlayerPrefs.GetInt("PeopleScored", 0);
        if (totalPeopleScored < 3)
            PlayerPrefs.SetInt("PeopleScored", totalPeopleScored + 1);

        highScoreNameObject.SetActive(false);
        leaderboardsParentObject.SetActive(true);
        ShowScores();
    }

    public void OnCloseClicked()
    {
        SceneManager.UnloadSceneAsync(4);
    }

    public void OnNameDownClick(int num)
    {
        ChangeName(num, -1);
    }
    public void OnNameUpClick(int num)
    {
        ChangeName(num, 1);
    }

    private void ChangeName(int num, int direction)
    {
        char nextChar = GetChar(num);
        int index = availableChars.IndexOf(nextChar);
        index = direction > 0 ? index + 1 : index - 1;
        if (index >= availableChars.Count)
            nextChar = availableChars[0];
        else if (index < 0)
            nextChar = availableChars[availableChars.Count - 1];
        else
            nextChar = availableChars[index];
        string name = highScoreName.text;
        if (num == 1)
            name = nextChar + "  " + name[3] + "  " + name[6];
        else if (num == 2)
            name = name[0] + "  " + nextChar + "  " + name[6];
        else
            name = name[0] + "  " + name[3] + "  " + nextChar;
        highScoreName.text = name;
    }

    public void OnClearLeaderboardsClick()
    {
        PlayerPrefs.DeleteKey("PeopleScored");
        ShowScores();
    }

    private void OnDestroy()
    {
        IsOpened = false;
    }

    private char GetChar(int num)
    {
        string text = highScoreName.text;
        if (num == 1)
            return text[0];
        else if (num == 2)
            return text[3];
        else
            return text[6];
    }
}
