using UnityEngine;

public class WorldClock : MonoBehaviour
{

    [Header("ClockSpeed")]
    [SerializeField] float secondsPerSecond = 1;

    [Header("Events")]
    [SerializeField] EventInt daysEvent;
    [SerializeField] EventInt hoursEvent;
    [SerializeField] EventInt minutesEvent;
    [SerializeField] EventInt secondsEvent;
    [SerializeField] EventFloat dayProgression;
    [SerializeField] EventWeekday weekday;

    [Header("Debug")]
    float t;
    float timeSample;

    int seconds;
    int minutes;
    int hours;
    int days;

    int prevSeconds = -1;
    int prevMinutes = -1;
    int prevHours = -1;
    int prevDays = -1;

    float dayProg;
    Weekday day;

    static WorldClock instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.Log("Multiple instances of WorldClock in the scene. Destroying duplicates.");
            Destroy(gameObject);
        }

        // Start on day 1, 07:00
        t = 86400 + 7 * 3600;
    }

    void Update()
    {
        t += Time.deltaTime * secondsPerSecond;
        timeSample = t;

        days = (int)(timeSample / 86400);
        timeSample -= days * 86400;

        dayProg = timeSample / 86400;

        hours = (int)(timeSample / 3600);
        timeSample -= hours * 3600;

        minutes = (int)(timeSample / 60);
        timeSample -= minutes * 60;

        seconds = (int)(timeSample);

        DetermineWeekday();
        UpdateEvents();
    }

    void DetermineWeekday()
    {
        day = new Weekday((days % 7)-1);
    }

    void UpdateEvents()
    {
        if (days != prevDays)
        {
            daysEvent.Value = days;
            prevDays = days;
        }

        if (hours != prevHours)
        {
            hoursEvent.Value = hours;
            prevHours = hours;
        }

        if (minutes != prevMinutes)
        {
            minutesEvent.Value = minutes;
            prevMinutes = minutes;
        }

        if (seconds != prevSeconds)
        {
            secondsEvent.Value = seconds;
            prevSeconds = seconds;
        }

        dayProgression.Value = dayProg;
        weekday.Value = day;
    }
}
