using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBase : MonoBehaviour
{
    public static MainBase Instance { get; private set; }

    Stats MyStats;
    [SerializeField] Slider healthSlider;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DayNightCycle.Instance.DayFunctions += OnDay;
        MyStats = GetComponent<Stats>();
        MyStats.OnDeath += OnDeath;
        MyStats.OnHealthChanged += OnHealthChanged;
        healthSlider.maxValue = MyStats.MaxHealth;
        healthSlider.value = MyStats.MaxHealth;
    }

    void OnDay()
    {
        MyStats.FullHeal();
    }

    void OnDeath()
    {
        GetComponent<AudioSource>().Play();
        ScoreManagement.CheckNewHighScore();
        ScoreManagement.GameEndedMessage = "You Lose";
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    void OnHealthChanged()
    {
        healthSlider.value = MyStats.Health;
    }

    private void Update()
    {
        OnHealthChanged();
    }
}
