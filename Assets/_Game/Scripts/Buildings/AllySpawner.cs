using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : MonoBehaviour
{
    public GameObject AllyPrefab;
    public int AllyCost;
    public float TimerPerSummon;
    public int MaxAllyCount;

    int allyCount;
    Coroutine summon;
    bool isCoroutineRunning = false;
    Stats stats;

    void Start()
    {
        stats = GetComponent<Stats>();

        allyCount = 0;
        stats.OnDeath += OnDeath;
        DayNightCycle.Instance.DayFunctions += OnDayStart;
        DayNightCycle.Instance.NightFunctions += OnNightStart;
        if (DayNightCycle.Instance.IsNight)
            OnNightStart();
    }

    bool TrySummonUnit()
    {
        if (allyCount == MaxAllyCount || !MoneyManager.Instance.Purchase(AllyCost))
            return false;
        Instantiate(AllyPrefab, transform.position, Quaternion.identity);
        allyCount++;
        return true;
    }

    IEnumerator Summon()
    {
        while (true)
        {
            if (TrySummonUnit())
                yield return new WaitForSeconds(TimerPerSummon);
            else
                yield return null;
        }
    }

    public void OnNightStart()
    {
        if (!isCoroutineRunning)
        {
            isCoroutineRunning = true;
            summon = StartCoroutine(Summon());
        }
    }

    public void OnDayStart()
    {
        if (isCoroutineRunning)
        {
            StopCoroutine(summon);
            isCoroutineRunning = false;
        }
    }

    public void OnDeath()
    {
        StopCoroutine(summon);
        Destroy(gameObject);
    }


}
