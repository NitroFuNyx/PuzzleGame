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

    #region Events Declaration
    public event Action OnTimerFinished;

    public event Action<float> OnStopwatchStoped;
    #endregion Events Declaration

    private void Start()
    {
        OnTimerFinished += TimerFinished;

        OnStopwatchStoped += StopwatchStopped;
    }

    private void OnDestroy()
    {
        OnTimerFinished -= TimerFinished;

        OnStopwatchStoped -= StopwatchStopped;
    }

    private void TimerFinished()
    {
        Debug.Log($"Timer Finished");
    }

    private void StopwatchStopped(float stopwatchValue)
    {
        Debug.Log($"Stopwatch stoped {stopwatchValue}");
    }

    [ContextMenu("Start Timer")]
    public void StartTimer(float startTimerValue, TextMeshProUGUI timerText, Action OnTimerFinished)
    {
        //float startTimerValue = testTimerStartValue;
        StartCoroutine(StartTimerCoroutine(startTimerValue, timerText));
    }

    [ContextMenu("Start Stopwatch")]
    public void StartStopwatch(/*float startStopwatchValue*/)
    {
        float startStopwatchValue = testStopwatchStartValue;
        stopwatchActive = true;
        StartCoroutine(StartStopwatchCoroutine(startStopwatchValue));
    }

    [ContextMenu("Stop Stopwatch")]
    public void StopStopwatch()
    {
        stopwatchActive = false;
    }

    private string GetHoursAndMinutesAmount(int currentTimeValue)
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

    private string GetSecondsAmount(int currentTimeValue)
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

    private IEnumerator StartTimerCoroutine(float startTimerValue, TextMeshProUGUI timerText)
    {
        float currentTimerValue = startTimerValue;

        while(currentTimerValue > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentTimerValue--;
            timerText.text = $"{GetHoursAndMinutesAmount((int)currentTimerValue)}:{GetSecondsAmount((int)currentTimerValue)}";
        }

        OnTimerFinished?.Invoke();
    }

    private IEnumerator StartStopwatchCoroutine(float startStopwatchValue)
    {
        float currentStopwatchValue = startStopwatchValue;

        while (stopwatchActive)
        {
            yield return new WaitForSeconds(1f);
            currentStopwatchValue++;
            testStopwatchText.text = $"Stopwatch {GetHoursAndMinutesAmount((int)currentStopwatchValue)}:{GetSecondsAmount((int)currentStopwatchValue)}";
        }

        OnStopwatchStoped?.Invoke(currentStopwatchValue);
    }
}
