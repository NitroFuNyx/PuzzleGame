using UnityEngine;
using System;
using TMPro;
using Zenject;

public class MiniGameUI : MainCanvasPanel
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI currentGameTimerText;
    [SerializeField] private TextMeshProUGUI delayGameTimerTitleText;
    [SerializeField] private TextMeshProUGUI startGameDelayTimerText;
    [SerializeField] private TextMeshProUGUI coinsText;

    private TimersManager _timersManager;
    private ResourcesManager _resourcesManager;

    private void Start()
    {
        SubscribeOnEvents();

        coinsText.text = "0";
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(TimersManager timersManager, ResourcesManager resourcesManager)
    {
        _timersManager = timersManager;
        _resourcesManager = resourcesManager;
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

    private void SubscribeOnEvents()
    {
        _resourcesManager.OnLevelCoinsAmountChanged += OnLevelCoinsAmountChanged_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _resourcesManager.OnLevelCoinsAmountChanged -= OnLevelCoinsAmountChanged_ExecuteReaction;
    }

    private void OnLevelCoinsAmountChanged_ExecuteReaction(int coinsAmount)
    {
        coinsText.text = $"{coinsAmount}";
    }
}
