using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    [Tooltip("In seconds")]
    public float SpawnTime;

    public int MinimumDayToStartSpawn;
    [Tooltip("The bigger the number, the smaller the timer will get each day")]
    [Range(0.0001f, 0.1f)]
    public float DayMultiplier;


    Stats myStats;
    bool isDestroyed = false;
    bool startedSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        DayNightCycle.Instance.NightFunctions += OnNightStart;
        DayNightCycle.Instance.DayFunctions += OnDayStart;
        myStats = GetComponent<Stats>();
        myStats.OnDeath += OnDeath;
        if (MinimumDayToStartSpawn <= 1)
        {
            StartCoroutine(Spawn());
            startedSpawning = true;
        }
    }

    private void OnDeath()
    {
        isDestroyed = true;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (MinimumDayToStartSpawn == DayNightCycle.Instance.DayCounter && !startedSpawning)
        {
            StartCoroutine(Spawn());
            startedSpawning = true;
        }
    }

    void OnDayStart()
    {
        if (MinimumDayToStartSpawn == DayNightCycle.Instance.DayCounter && !startedSpawning)
        {
            StartCoroutine(Spawn());
            startedSpawning = true;
            DayNightCycle.Instance.DayFunctions -= OnDayStart;
        }
    }

    void OnNightStart()
    {
        if (startedSpawning)
            SpawnTime = SpawnTime - SpawnTime * DayMultiplier * DayNightCycle.Instance.DayCounter;
    }

    IEnumerator Spawn()
    {
        print("here");
        while (!isDestroyed)
        {
            if (DayNightCycle.Instance.IsNight)
                Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnTime);
        }

    }
}
