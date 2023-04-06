using UnityEngine;
using System;
using System.Collections;
using TMPro;
using DG.Tweening;
using Zenject;

public class MiniGameUI : MainCanvasPanel
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI currentGameTimerText;
    [SerializeField] private TextMeshProUGUI timerBonusText;
    [SerializeField] private TextMeshProUGUI delayGameTimerTitleText;
    [SerializeField] private TextMeshProUGUI startGameDelayTimerText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [Header("Delays")]
    [Space]
    [SerializeField] private float hideBonusTimeTextDelay = 2f;
    [Header("Durations")]
    [Space]
    [SerializeField] private float bonusTimeTextChangeAlphaDurationMax = 1f;
    [SerializeField] private float bonusTimeTextChangeAlphaDurationMin = 0.01f;
    [Header("Panels")]
    [Space]
    [SerializeField] private KitchenMiniGameBonusTimersPanel kitchenMiniGameBonusTimersPanel;
    [SerializeField] private PanelActivationManager gameFinishedPanelActivationManager;
    [Header("Buttons")]
    [Space]
    [SerializeField] private WatchVideoButton watchVideoButton;

    private TimersManager _timersManager;
    private ResourcesManager _resourcesManager;

    private MiniGameFinishedPanel miniGameFinishedPanel;

    private void Start()
    {
        SubscribeOnEvents();

        coinsText.text = "0";
        timerBonusText.text = "";

        miniGameFinishedPanel = gameFinishedPanelActivationManager.GetComponent<MiniGameFinishedPanel>();
        HideGameFinishedPanel();
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

    public void ShowBonusTime(float time)
    {
        timerBonusText.text = $"+ {_timersManager.GetHoursAndMinutesAmount((int)time)}:{_timersManager.GetSecondsAmount((int)time)}";
        _timersManager.IncreaseMiniGameTimerValue(time, currentGameTimerText);
        timerBonusText.DOFade(1f, bonusTimeTextChangeAlphaDurationMin);
        StartCoroutine(HideBonusTimeTextCoroutine());
    }

    public void ResetUIData()
    {
        coinsText.text = $"{_resourcesManager.CurrentLevelCoinsAmount}";
        currentGameTimerText.text = "";
        timerBonusText.text = "";
        delayGameTimerTitleText.text = "";
        startGameDelayTimerText.text = "";
        kitchenMiniGameBonusTimersPanel.ResetData();
        timerBonusText.DOFade(0f, bonusTimeTextChangeAlphaDurationMax);
    }

    public void ShowGameFinishedPanel()
    {
        miniGameFinishedPanel.SetCoinsText(_resourcesManager.CurrentLevelCoinsAmount);
        watchVideoButton.VideoWatched = false;
        gameFinishedPanelActivationManager.ShowPanel();
        ResetUIData();
    }

    public void HideGameFinishedPanel()
    {
        gameFinishedPanelActivationManager.HidePanel();
    }

    private void SubscribeOnEvents()
    {
        _resourcesManager.OnLevelCoinsAmountChanged += OnLevelCoinsAmountChanged_ExecuteReaction;
        _resourcesManager.OnAdditionalCoinsAdedAsAdReward += OnAdditionalCoinsAsAdRewardAdded_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _resourcesManager.OnLevelCoinsAmountChanged -= OnLevelCoinsAmountChanged_ExecuteReaction;
        _resourcesManager.OnAdditionalCoinsAdedAsAdReward -= OnAdditionalCoinsAsAdRewardAdded_ExecuteReaction;
    }

    private void OnLevelCoinsAmountChanged_ExecuteReaction(int coinsAmount)
    {
        coinsText.text = $"{coinsAmount}";
    }

    private void OnAdditionalCoinsAsAdRewardAdded_ExecuteReaction(int coinsAmount)
    {
        miniGameFinishedPanel.SetCoinsText(coinsAmount);
    }

    private IEnumerator HideBonusTimeTextCoroutine()
    {
        yield return new WaitForSeconds(hideBonusTimeTextDelay);
        timerBonusText.DOFade(0f, bonusTimeTextChangeAlphaDurationMax);
    }
}
