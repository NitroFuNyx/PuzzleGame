using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class TimersManager : MonoBehaviour
{
    [Header("Test Values")]
    [Space]
    [SerializeField] private float testTimerStartValue = 1f;
    [Space]
    [SerializeField] private float testStopwatchStartValue = 1f;
    [Header("Test Text")]
    [Space]
    [SerializeField] private TextMeshProUGUI testTimerText;
    [Space]
    [SerializeField] private TextMeshProUGUI testStopwatchText;

    private const int SubdivivisionsInTimeUnitAmount = 60;

    private bool stopwatchActive = false;

    private float currentTimerValue;

    private Coroutine timerCoroutine;

    #region Events Declaration
    //public event Action OnTimerFinished;

    //public event Action<float> OnStopwatchStoped;
    #endregion Events Declaration

    //private void Start()
    //{
    //    OnTimerFinished += TimerFinished;

    //    OnStopwatchStoped += StopwatchStopped;
    //}

    //private void OnDestroy()
    //{
    //    OnTimerFinished -= TimerFinished;

    //    OnStopwatchStoped -= StopwatchStopped;
    //}

    //private void TimerFinished()
    //{
    //    Debug.Log($"Timer Finished");
    //}

    //private void StopwatchStopped(float stopwatchValue)
    //{
    //    Debug.Log($"Stopwatch stoped {stopwatchValue}");
    //}

    public void StartTimer(float startTimerValue, TextMeshProUGUI timerText, Action OnTimerFinished)
    {
        timerCoroutine =  StartCoroutine(StartTimerCoroutine(startTimerValue, timerText, OnTimerFinished));
    }

    public void StartStopwatch(float startStopwatchValue, TextMeshProUGUI timerText, Action<float> OnStopwatchStopped)
    {
        startStopwatchValue = testStopwatchStartValue;
        stopwatchActive = true;
        StartCoroutine(StartStopwatchCoroutine(startStopwatchValue, timerText, OnStopwatchStopped));
    }

    public void StopTimer()
    {
        StopCoroutine(timerCoroutine);
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
    }

    public string GetHoursAndMinutesAmount(int currentTimeValue)
    {
        string amountString = "";

        int amount = currentTimeValue / SubdivivisionsInTimeUnitAmount;
        int amountOfHours = 0;
        int amountOfMinutes = 0;

        if(amount / SubdivivisionsInTimeUnitAmount > 0)
        {
            amountOfHours = amount / SubdivivisionsInTimeUnitAmount;
            amountOfMinutes = amount % SubdivivisionsInTimeUnitAmount;

            string hours = $"{amountOfHours}";
            string minutes = $"{amountOfMinutes}";

            if(amountOfHours < 10)
            {
                hours = $"0{amountOfHours}";
            }
            if(amountOfMinutes < 10)
            {
                minutes = $"0{amountOfMinutes}";
            }
            amountString = $"{hours}:{minutes}";
        }
        else
        {
            amountString = $"{amount}";
            if (amount < 10)
            {
                amountString = $"0{amount}";
            }
        }

        return amountString;
    }

    public string GetSecondsAmount(int currentTimeValue)
    {
        string amountString;

        int amount = currentTimeValue % SubdivivisionsInTimeUnitAmount;

        amountString = $"{amount}";
        if (amount < 10)
        {
            amountString = $"0{amount}";
        }

        return amountString;
    }

    public void IncreaseMiniGameTimerValue(float time, TextMeshProUGUI timerText)
    {
        currentTimerValue += time;
        timerText.text = $"{GetHoursAndMinutesAmount((int)currentTimerValue)}:{GetSecondsAmount((int)currentTimerValue)}";
    }

    private IEnumerator StartTimerCoroutine(float startTimerValue, TextMeshProUGUI timerText, Action OnTimerFinished)
    {
        currentTimerValue = startTimerValue;

        while(currentTimerValue > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentTimerValue--;
            timerText.text = $"{GetHoursAndMinutesAmount((int)currentTimerValue)}:{GetSecondsAmount((int)currentTimerValue)}";
        }

        OnTimerFinished?.Invoke();
        //OnTimerFinished?.Invoke();
    }

    private IEnumerator StartStopwatchCoroutine(float startStopwatchValue, TextMeshProUGUI timerText, Action<float> OnStopwatchStopped)
    {
        float currentStopwatchValue = startStopwatchValue;

        while (stopwatchActive)
        {
            yield return new WaitForSeconds(1f);
            currentStopwatchValue++;
            timerText.text = $"{GetHoursAndMinutesAmount((int)currentStopwatchValue)}:{GetSecondsAmount((int)currentStopwatchValue)}";
        }

        OnStopwatchStopped?.Invoke(currentStopwatchValue);
        //OnStopwatchStoped?.Invoke(currentStopwatchValue);
    }
}
