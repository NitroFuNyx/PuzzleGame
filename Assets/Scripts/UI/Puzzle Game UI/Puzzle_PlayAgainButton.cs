using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Puzzle_PlayAgainButton : ButtonInteractionHandler
{
    private PuzzleGameUI _puzzleGameUI;
    private CurrentGameManager _currentGameManager;
    private AudioManager _audioManager;
    private AdsInitializer _adsInitializer;
    private RewardedAdsButton rewardedAdsButton;

    private void Awake()
    {
        if (TryGetComponent(out RewardedAdsButton button))
        {
            rewardedAdsButton = button;
            rewardedAdsButton.SetStartData();
        }
    }

    private void Start()
    {
        if (rewardedAdsButton)
        {
            rewardedAdsButton.OnRewardReadyToBeGranted += GrandReward;
        }
        if (ButtonComponent == null)
        {
            ButtonComponent = GetComponent<Button>();
            ButtonComponent.onClick.AddListener(ButtonActivated);
        }
    }

    private void OnDestroy()
    {
        if (rewardedAdsButton)
            rewardedAdsButton.OnRewardReadyToBeGranted -= GrandReward;
    }

    #region Zenject
    [Inject]
    private void Construct(CurrentGameManager currentGameManager, AudioManager audioManager, AdsInitializer adsInitializer, PuzzleGameUI puzzleGameUI)
    {
        _currentGameManager = currentGameManager;
        _audioManager = audioManager;
        _adsInitializer = adsInitializer;
        _puzzleGameUI = puzzleGameUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if (rewardedAdsButton && _adsInitializer.AdsCanBeLoaded)
        {
            ShowAnimation_ButtonPressed();
            StartCoroutine(ActivateDelayedButtonMethodCoroutine(rewardedAdsButton.ShowAd));
        }
    }

    private void GrandReward()
    {
        _currentGameManager.ActivateGameLevelEnvironment(0, GameLevelTypes.Puzzle);
        _puzzleGameUI.ShowMainModePanel();
    }
}
