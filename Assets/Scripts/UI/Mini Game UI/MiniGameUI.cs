using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class MiniGameUI : MainCanvasPanel
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI currentGameTimerText;
    [SerializeField] private TextMeshProUGUI delayGameTimerTitleText;
    [SerializeField] private TextMeshProUGUI startGameDelayTimerText;

    private TimersManager _timersManager;

    #region Zenject
    [Inject]
    private void Construct(TimersManager timersManager)
    {
        _timersManager = timersManager;
    }
    #endregion Zenject

    public void UpdateStartGameDelayTimerText(float counter)
    {
        startGameDelayTimerText.text = $"{counter}";
    }

    public void HideDelayTimerText()
    {
        delayGameTimerTitleText.text = "";
        startGameDelayTimerText.text = "";
    }

    public void StartCurrentGameTimer(float timerValue, Action OnTimerFinished)
    {
        _timersManager.StartTimer(timerValue, currentGameTimerText, OnTimerFinished);
    }
}
