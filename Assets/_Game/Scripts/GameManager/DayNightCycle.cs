using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle Instance { get; private set; }
    public delegate void MidDayFunctions();
    public MidDayFunctions DayFunctions;
    public MidDayFunctions NightFunctions;
    public Transform DirectionalLight;
    public float TimeInSecondsForFullDay;
    public TMPro.TMP_Text Clock;

    float currentTime;
    bool pastNightTime; // prevent multiple night/day functions from calling more than once each time.

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = TimeInSecondsForFullDay;
        DirectionalLight.rotation = Quaternion.Euler((currentTime / (TimeInSecondsForFullDay)) * 360f, 0, 0);
        pastNightTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        DirectionalLight.rotation = Quaternion.Euler((currentTime / (TimeInSecondsForFullDay)) * 360f, 0, 0);

        if (currentTime >= TimeInSecondsForFullDay / 2 && !pastNightTime)
            Night();
        else if (currentTime >= TimeInSecondsForFullDay)
            Day();

        Clock.text = PersentageToClock(currentTime / TimeInSecondsForFullDay, 6);
    }

    void Night()
    {
        pastNightTime = true;
        print("night");
        NightFunctions?.Invoke();
    }

    void Day()
    {
        currentTime -= TimeInSecondsForFullDay;
        pastNightTime = false;
        print("day");
        DayFunctions?.Invoke();

    }

    string PersentageToClock(float persentage, int hourOffset = 0)
    {
        float hours = persentage * 24;
        hours = (hours + hourOffset) % 24;
        float minutes = ((hours % 24) * 60) % 60;

        return string.Format("{0:D2}", (int)hours) + ":" + string.Format("{0:D2}", (int)minutes);
    }

    public void SkipDay()
    {
        currentTime = TimeInSecondsForFullDay / 2;
    }
}
