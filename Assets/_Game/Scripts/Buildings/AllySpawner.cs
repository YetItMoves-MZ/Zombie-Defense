using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : MonoBehaviour
{
    public GameObject AllyPrefab;
    public int AllyCost;
    public float TimerPerSummon;
    public int MaxAllyCount;

    [SerializeField] AudioClip destructionClip;

    Coroutine summon;
    bool isCoroutineRunning = false;
    Stats stats;
    List<AllyMovement> summonedAllies;

    void Start()
    {
        stats = GetComponent<Stats>();

        summonedAllies = new List<AllyMovement>();
        stats.OnDeath += OnDeath;
        DayNightCycle.Instance.DayFunctions += OnDayStart;
        DayNightCycle.Instance.NightFunctions += OnNightStart;
        if (DayNightCycle.Instance.IsNight)
            OnNightStart();
    }

    bool TrySummonUnit()
    {
        if (summonedAllies.Count == MaxAllyCount || !MoneyManager.Instance.Purchase(AllyCost))
            return false;
        GetComponent<AudioSource>().Play();

        GameObject summon = Instantiate(AllyPrefab, transform.position, Quaternion.identity);
        AllyMovement summonMovements = summon.GetComponent<AllyMovement>();

        summonedAllies.Add(summonMovements);
        summonMovements.OnSummonDeath += OnSummonDeath;
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

    public void OnSummonDeath(AllyMovement summon)
    {
        summonedAllies.Remove(summon);
    }

    public void OnDeath()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = destructionClip;
        audioSource.Play();
        StopCoroutine(summon);
        summonedAllies.Clear();
        Destroy(gameObject);
    }


}
